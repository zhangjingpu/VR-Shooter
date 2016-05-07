using UnityEngine;
using System.Collections;

//TODO:
/*
    I'll have to come in here and make an actual interface for this class,
        since it looks like a right PITA to use atm.

    -An enum for button differentiation, with automatic pressed resolution.
*/

//Attach this to both controllers
public class StickController : MonoBehaviour
{
    //Get Input keys
    public Valve.VR.EVRButtonId GripyButton     = Valve.VR.EVRButtonId.k_EButton_Grip;               //Side buttons
    public Valve.VR.EVRButtonId TriggerButton   = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;    //Obvious
    //Untested
    public Valve.VR.EVRButtonId MenuButton      = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;    //Above TP?
    public Valve.VR.EVRButtonId AButton         = Valve.VR.EVRButtonId.k_EButton_A;                  //? Perhaps dashboard only.
    public Valve.VR.EVRButtonId A0Button        = Valve.VR.EVRButtonId.k_EButton_Axis0;              //? Perhaps dashboard only.
    public Valve.VR.EVRButtonId A1Button        = Valve.VR.EVRButtonId.k_EButton_Axis1;              //? Perhaps dashboard only.
    public Valve.VR.EVRButtonId A2Button        = Valve.VR.EVRButtonId.k_EButton_Axis2;              //? Perhaps dashboard only.
    public Valve.VR.EVRButtonId A3Button        = Valve.VR.EVRButtonId.k_EButton_Axis3;              //? Perhaps dashboard only.
    public Valve.VR.EVRButtonId A4Button        = Valve.VR.EVRButtonId.k_EButton_Axis4;              //? Perhaps dashboard only.
    public Valve.VR.EVRButtonId BackButton      = Valve.VR.EVRButtonId.k_EButton_Dashboard_Back;     //? Perhaps dashboard only.
    public Valve.VR.EVRButtonId DDownButton     = Valve.VR.EVRButtonId.k_EButton_DPad_Down;          //Obvious
    public Valve.VR.EVRButtonId DLeftButton     = Valve.VR.EVRButtonId.k_EButton_DPad_Left;          //Obvious
    public Valve.VR.EVRButtonId DRightButton    = Valve.VR.EVRButtonId.k_EButton_DPad_Right;         //Obvious
    public Valve.VR.EVRButtonId DUpButton       = Valve.VR.EVRButtonId.k_EButton_DPad_Up;            //Obvious
    public Valve.VR.EVRButtonId MaxButton       = Valve.VR.EVRButtonId.k_EButton_Max;                //?
    public Valve.VR.EVRButtonId TouchpadButton  = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;   //Obvious
    public Valve.VR.EVRButtonId SystemButton    = Valve.VR.EVRButtonId.k_EButton_System;             //Below TP?

    //Get the controller
    [HideInInspector]
    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)TrackedObj.index); } }
    private SteamVR_TrackedObject TrackedObj;

    private float[] t_HeldFor = { 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f };

    /*
        Note: We can modify this using custom "controller states"
           that can change what the buttons do. Control customization, if you will. 
           we can change their function automatically in this class, and script
           with the default layout. 
    */
    /// <summary>
    /// Usable in-game buttons for Vive
    /// </summary>
    protected enum ActivatorButton
    { //on t_HeldFor....
        NONE, // 0
        MENU, // 1
        GRIPPY, TIRGGER, //2, 3
        TP_UP, TP_DOWN, //4, 5
        TP_LEFT, TP_RIGHT, //6, 7
        TP_CENTER //8
    }


    void Start ()
    {
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update ()
    {
	    if(Controller == null)
        {
            Debug.Log("Controller is not initialized");
            return;
        }

        ManageInputs();
    }

    /// <summary>
    /// Returns true if "ab" was pressed.
    /// </summary>
    bool isPressed(ActivatorButton ab)
    {
        switch(ab)
        {
            case ActivatorButton.TP_UP:     return Controller.GetPressDown(DUpButton);
            case ActivatorButton.MENU:      return Controller.GetPressDown(MenuButton); 
            case ActivatorButton.GRIPPY:    return Controller.GetPressDown(GripyButton);
            case ActivatorButton.TP_DOWN:   return Controller.GetPressDown(DDownButton); 
            case ActivatorButton.TP_LEFT:   return Controller.GetPressDown(DLeftButton); 
            case ActivatorButton.TP_RIGHT:  return Controller.GetPressDown(DRightButton);
            case ActivatorButton.TIRGGER:   return Controller.GetPressDown(TriggerButton); 
            case ActivatorButton.TP_CENTER: return Controller.GetPressDown(TouchpadButton);
            default: return false;
        }
    }
    /// <summary>
    /// Returns true if "ab" was released.
    /// </summary>
    bool isReleased(ActivatorButton ab)
    {
        switch (ab)
        {
            case ActivatorButton.TP_UP:     return Controller.GetPressUp(DUpButton);
            case ActivatorButton.MENU:      return Controller.GetPressUp(MenuButton);
            case ActivatorButton.GRIPPY:    return Controller.GetPressUp(GripyButton);
            case ActivatorButton.TP_DOWN:   return Controller.GetPressUp(DDownButton);
            case ActivatorButton.TP_LEFT:   return Controller.GetPressUp(DLeftButton);
            case ActivatorButton.TP_RIGHT:  return Controller.GetPressUp(DRightButton);
            case ActivatorButton.TIRGGER:   return Controller.GetPressUp(TriggerButton);
            case ActivatorButton.TP_CENTER: return Controller.GetPressUp(TouchpadButton);
            default: return false;
        }
    }
    /// <summary>
    /// Returns true if "ab" has been held for longer than the passed float.
    /// </summary>
    bool isHeld(ActivatorButton ab, float t_TimeHeld)
    {
        switch (ab)
        {
            case ActivatorButton.MENU:
                if (t_HeldFor[1] >= t_TimeHeld) return true; break;
            case ActivatorButton.GRIPPY:
                if (t_HeldFor[2] >= t_TimeHeld) return true; break;
            case ActivatorButton.TIRGGER:
                if (t_HeldFor[3] >= t_TimeHeld) return true; break;
            case ActivatorButton.TP_UP:
                if (t_HeldFor[4] >= t_TimeHeld) return true; break;
            case ActivatorButton.TP_DOWN:
                if (t_HeldFor[5] >= t_TimeHeld) return true; break;
            case ActivatorButton.TP_LEFT:
                if (t_HeldFor[6] >= t_TimeHeld) return true; break;
            case ActivatorButton.TP_RIGHT:
                if (t_HeldFor[7] >= t_TimeHeld) return true; break;
            case ActivatorButton.TP_CENTER:
                if (t_HeldFor[8] >= t_TimeHeld) return true; break;
        }

        return false;
    }


    void UpdateInputs()
    {
        if (Controller.GetPress(MenuButton)) { t_HeldFor[1] += Time.deltaTime; }
        else { t_HeldFor[1] = 0.0f; }
        if (Controller.GetPress(GripyButton)) { t_HeldFor[2] += Time.deltaTime; }
        else { t_HeldFor[1] = 0.0f; }
        if (Controller.GetPress(TriggerButton)) { t_HeldFor[3] += Time.deltaTime; }
        else { t_HeldFor[1] = 0.0f; }
        if (Controller.GetPress(DUpButton)) { t_HeldFor[4] += Time.deltaTime; }
        else { t_HeldFor[1] = 0.0f; }
        if (Controller.GetPress(DDownButton)) { t_HeldFor[5] += Time.deltaTime; }
        else { t_HeldFor[1] = 0.0f; }
        if (Controller.GetPress(DLeftButton)) { t_HeldFor[6] += Time.deltaTime; }
        else { t_HeldFor[1] = 0.0f; }
        if (Controller.GetPress(DRightButton)) { t_HeldFor[7] += Time.deltaTime; }
        else { t_HeldFor[1] = 0.0f; }
        if (Controller.GetPress(TouchpadButton)) { t_HeldFor[8] += Time.deltaTime; }
        else { t_HeldFor[1] = 0.0f; }
    }

    //Interactions
    void ManageInputs()
    {
        UpdateInputs();
        //DebugInputs();
    }

    //Test a few buttons
    void DebugInputs()
    {
        if (Controller.GetPressDown(GripyButton)) print("Grip Button Down");
        if (Controller.GetPressUp(GripyButton)) print("Grip Button Up");
        if (Controller.GetPress(GripyButton)) print("Grip Button Pressed"); //Held

        if (Controller.GetPressDown(TriggerButton)) print("Trigger Button Down");
        if (Controller.GetPressUp(TriggerButton)) print("Trigger Button Up");
        if (Controller.GetPress(TriggerButton)) print("Trigger Button Pressed"); //held

        //Untested
        if (Controller.GetPressDown(MenuButton)) print("MenuButton");
        if (Controller.GetPressDown(AButton)) print("AButton");
        if (Controller.GetPressDown(A0Button)) print("A0Button");
        if (Controller.GetPressDown(A1Button)) print("A1Button");
        if (Controller.GetPressDown(A2Button)) print("A2Button");
        if (Controller.GetPressDown(A3Button)) print("A3Button");
        if (Controller.GetPressDown(A4Button)) print("A4Button");
        if (Controller.GetPressDown(BackButton)) print("BackButton");
        if (Controller.GetPressDown(DDownButton)) print("DDownButton");
        if (Controller.GetPressDown(DLeftButton)) print("DLeftButton");
        if (Controller.GetPressDown(DRightButton)) print("DRightButton");
        if (Controller.GetPressDown(DUpButton)) print("DUpButton");
        if (Controller.GetPressDown(MaxButton)) print("MaxButton");
        if (Controller.GetPressDown(TouchpadButton)) print("TouchpadButton");
        if (Controller.GetPressDown(SystemButton)) print("SystemButton");
    }
}
