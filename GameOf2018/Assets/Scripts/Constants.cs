using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{

    public static class PlayerInput
    {
        public static bool IsPressingLeft
        {
            get
            {
                return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.LeftArrow);
            }
        }

        public static bool IsPressingRight
        {
            get
            {
                return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Semicolon) || Input.GetKey(KeyCode.RightArrow);
            }
        }

        public static bool IsPressingUp
        {
            get
            {
                return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.O) || Input.GetKey(KeyCode.UpArrow);
            }
        }

        public static bool IsPressingDown
        {
            get
            {
                return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.DownArrow);
            }
        }

        public static bool IsPressingSpace
        {
            get
            {
                return Input.GetKey(KeyCode.Space);
            }
        }

        public static bool IsPressingAirDash
        {
            get
            {
                return Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            }
        }
    }
}
