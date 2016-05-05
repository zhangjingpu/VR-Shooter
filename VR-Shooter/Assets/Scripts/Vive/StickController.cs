using UnityEngine;
using System.Collections;

public class StickController : MonoBehaviour
{
    //Get Input keys
    private Valve.VR.EVRButtonId GripyButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    //Get the controller
    private SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)TrackedObj.index); } }
    private SteamVR_TrackedObject TrackedObj;

    private GameObject GrabedObject;

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

    //Interactions
    void ManageInputs()
    {
        //Grab
        if (Controller.GetPressDown(GripyButton) && GrabedObject != null) GrabedObject.transform.parent = this.transform;

        DebugInputs();
    }

    //Test a few buttons
    void DebugInputs()
    {
        if (Controller.GetPressDown(GripyButton)) print("Grip Button Down");
        if (Controller.GetPressUp(GripyButton)) print("Grip Button Up");
        if (Controller.GetPress(GripyButton)) print("Grip Button Pressed");

        if (Controller.GetPressDown(TriggerButton)) print("Trigger Button Down");
        if (Controller.GetPressUp(TriggerButton)) print("Trigger Button Up");
        if (Controller.GetPress(TriggerButton)) print("Trigger Button Pressed");
    }

    //Collisions
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) GrabedObject = other.gameObject;
    }

    void OnTriggerStay(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) GrabedObject = null;
    }
}
