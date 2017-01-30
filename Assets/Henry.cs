using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState {idle, right, left}
public enum JumpState {grounded, jump, raising, falling}

public class Henry : MonoBehaviour {

    public MoveState myMove = MoveState.idle;
    public JumpState myJump = JumpState.grounded;
    public float runSpeed = 1;
    public float jumpForce = 1;
    public float doubleJumpMod = .5f;
    int jump = 1;

    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        myMove = MoveState.idle;
        myJump = JumpState.grounded;
	}
	
    void FixedUpdate()
    {
        if (myMove == MoveState.right)
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        else if (myMove == MoveState.left)
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
        
        if (myJump == JumpState.jump)
        {
            if (jump < 3)
            {
                rb.AddForce(new Vector2(0, jumpForce / jump));
                jump++;
            }
        }
            
    }


}
