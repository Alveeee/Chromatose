using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState {idle, right, left}
public enum JumpState {grounded, jump, rising, falling}

public enum GameColor {Black, Grey, Grey2, White}
public class Henry : MonoBehaviour {

    public MoveState myMove = MoveState.idle;
    public JumpState myJump = JumpState.grounded;
    public bool crouched = false;
    public float runSpeed = 1;
    public float jumpForce = 1;
    public float doubleJumpMod = .5f;
    float groundDetectBuffer = .05f;
    public GameColor myGameColor = GameColor.Grey;
    int jump = 1;
    public float deaccelerationMultiplyer = 1;
    BoxCollider2D characterCollider;
    Gui gui;
    Devon devon;

    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        myMove = MoveState.idle;
        myJump = JumpState.grounded;
        characterCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        devon = GameObject.FindWithTag("Player").GetComponent<Devon>();
        gui = devon.GetComponent<Gui>();
    }
	
    void FixedUpdate()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.TransformPoint(new Vector2(0, -characterCollider.size.y / 2 + characterCollider.offset.y + .01f)), Vector2.down, groundDetectBuffer, LayerMask.GetMask(new string[]{GameColor.Black.ToString(), myGameColor.ToString()}));
        //Debug.DrawRay(transform.TransformPoint(new Vector2(0, -characterCollider.size.y / 2 + characterCollider.offset.y)), Vector2.down, Color.green, groundDetectBuffer);
        if (myJump == JumpState.jump)
        {
            if (jump < 3 && rb.velocity.y < 5)
            {
                Debug.Log(rb.velocity.y);
                rb.AddForce(new Vector2(0, jumpForce / jump), ForceMode2D.Impulse);
                if (rb.velocity.y > 0)
                    myJump = JumpState.rising;
                else
                    myJump = JumpState.falling;
                jump++;
            }
        }
        else if (myJump == JumpState.rising)
        {
            if (rb.velocity.y <= 0)
            {
                myJump = JumpState.falling;
            }
        }
        else if (myJump == JumpState.falling)
        {
        }

        if (groundHit)
        {
            if (Mathf.Abs(rb.velocity.y) < .1f)
            {
                if (myJump != JumpState.grounded)
                {
                    myJump = JumpState.grounded;
                }

                if (jump > 1)
                    jump = 1;
            }
         
            if (myMove == MoveState.right)
                rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            else if (myMove == MoveState.left)
                rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            else
            {
                if (Mathf.Abs(rb.velocity.x) > .1f)
                    rb.velocity = new Vector2(rb.velocity.x * (1 - (deaccelerationMultiplyer / 10)), 0);
                else
                    rb.velocity = new Vector2(0, rb.velocity.y);
            }
                
        }
        
     }
}
