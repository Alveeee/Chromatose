using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui : MonoBehaviour {

    public float inputButtonSize = 1;
    Rect master;
    Rect[] inputButtons = new Rect[4];

    void Start()
    {
        master = new Rect(Screen.dpi * inputButtonSize, Screen.height - (2*Screen.dpi*inputButtonSize), Screen.width - (2*Screen.dpi*inputButtonSize), 2*Screen.dpi*inputButtonSize);
        inputButtons[0] = new Rect(0, master.y, Screen.width - master.xMax, master.height);
        inputButtons[1] = new Rect(master.xMax, master.y, inputButtons[0].width, master.height);
        inputButtons[2] = new Rect(master.x, master.y, master.width, master.height/2);
        inputButtons[3] = new Rect(master.x, inputButtons[2].yMax, master.width, master.height/2);

    }
	// Use this for initialization
    void OnGUI ()
    {
        foreach (Rect button in inputButtons)
        {
            GUI.RepeatButton(button, "");
        }
    }
}
