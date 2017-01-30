using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui : MonoBehaviour {

    public float inputButtonSize = 1;
    Rect master;
    Rect[] inputButtons = new Rect[4];
    Henry henry;
    bool[] buttonsPressed = new bool[4];
    public bool firstJumpFrame = true;

    void Start()
    {
        henry = GameObject.FindWithTag("Character").GetComponent<Henry>();
        master = new Rect(Screen.dpi * inputButtonSize, Screen.height - (2*Screen.dpi*inputButtonSize), Screen.width - (2*Screen.dpi*inputButtonSize), 2*Screen.dpi*inputButtonSize);
        inputButtons[0] = new Rect(0, master.y, Screen.width - master.xMax, master.height);
        inputButtons[1] = new Rect(master.xMax, master.y, inputButtons[0].width, master.height);
        inputButtons[2] = new Rect(master.x, master.y, master.width, master.height/2);
        inputButtons[3] = new Rect(master.x, inputButtons[2].yMax, master.width, master.height/2);

    }

    void Update()
    {
        if (buttonsPressed[0])
        {
            if (!buttonsPressed[1])
            {
                if (henry.myMove != MoveState.left)
                    henry.myMove = MoveState.left;
            }
        }
        else if (buttonsPressed[1])
        {
            if (henry.myMove != MoveState.right)
                henry.myMove = MoveState.right;
        }
        else
        {
            if (henry.myMove != MoveState.idle)
                henry.myMove = MoveState.idle;
        }
        if (buttonsPressed[2])
        {
            if (firstJumpFrame)
            {
                firstJumpFrame = false;
                if (henry.myJump != JumpState.jump)
                {
                    henry.myJump = JumpState.jump;
                }
            }
        }
        else
        {
            if (!firstJumpFrame)
                firstJumpFrame = true;
        }
    }

	// Use this for initialization
    void OnGUI ()
    {
        for(int i = 0; i < 4; i++)
        {
            if (GUI.RepeatButton(inputButtons[i], ""))
            {
                if (!buttonsPressed[i])
                    buttonsPressed[i] = true;
            }
            else if (buttonsPressed[i])
                buttonsPressed[i] = false;

        }
    }
}
