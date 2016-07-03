using UnityEngine;
using System.Collections;



public enum Swipe
{
    Down,
    Up,
    Left,
    Right
}


public class CrossPlatformInput {

    public static bool GetSwipe(Swipe swipe)
    {
        switch (swipe)
        {
            case Swipe.Up:
                return Input.GetButtonDown("Jump");
        }

        return false;
    }
}
