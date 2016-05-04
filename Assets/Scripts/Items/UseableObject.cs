using UnityEngine;
using System.Collections;

/*
Author: A. Connor Adam

    This class is for objects that can be picked up, and makes an interface
    that allows them to have instant button behavior.

Last Edit: A. Connor Adam
*/

public class UseableObject : StickController
{ 
    //The button that activates the object being held.
    protected enum ActivaterButton
    {
        NONE,
        TRIGGER,
        GRIPPY,
        MENU
    };

    protected bool isActivtorPressed(ActivaterButton b)
    {
        switch(b)
        {
            case ActivaterButton.TRIGGER:
                if (Controller.GetPress(TriggerButton))
                    return true;
                break;
            case ActivaterButton.GRIPPY:
                if (Controller.GetPress(GripyButton))
                    return true;
                break;
            case ActivaterButton.MENU:
                if (Controller.GetPress(MenuButton))
                    return true;
                break;
        }

        return false;
    }
}
