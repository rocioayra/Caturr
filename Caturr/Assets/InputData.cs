using UnityEngine;
using System;

[Serializable]
public struct InputData
{
    //Basic Movement
    public float hMovement;
    public float vMovement;

    //Mouse rotation
    public float verticalMouse;
    public float horizontalMouse;

    //Extra movement
    public bool dash;
    public bool jump;

    public void getInput()
    {
        //Basic movement
        hMovement = Input.GetAxis("Horizontal");
        vMovement = Input.GetAxis("Vertical");

        //Mouse/Joystick rotation
        verticalMouse = Input.GetAxis("Mouse Y");
        horizontalMouse = Input.GetAxis("Mouse X");

        //Extra movement
        dash = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetButtonDown("Jump");
    }
}