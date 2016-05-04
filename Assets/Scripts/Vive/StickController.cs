using UnityEngine;
using System.Collections;


public class StickController : MonoBehaviour
{
    //Get Input keys
    public Valve.VR.EVRButtonId GripyButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public Valve.VR.EVRButtonId TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    //Get the controller
    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)TrackedObj.index); } }
    private SteamVR_TrackedObject TrackedObj;

    public GameObject GrabedObject;

    private Vector3 lastPos;

    [HideInInspector]
    public Vector3 vel;


    void Start ()
    {
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
        lastPos = transform.position;
	}

	void Update ()
    {
	    if(Controller == null)
        {
            Debug.Log("Controller is not initialized");
            return;
        }

        vel = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;

        ManageInputs();
    }

    //Interactions
    void ManageInputs()
    {
        //Grab
        if(GrabedObject != null)
        {
            if (Controller.GetPress(GripyButton) && GrabedObject != null)
            {
                if (GrabedObject.GetComponent<Rigidbody>() != null) GrabedObject.GetComponent<Rigidbody>().isKinematic = true;
                GrabedObject.transform.parent = transform; //Turns to a child also
            }
            else
            {
                GrabedObject.transform.SetParent(null);
                GrabedObject.GetComponent<Rigidbody>().isKinematic = false;
                GrabedObject.GetComponent<Rigidbody>().AddForce(vel * 100);
                GrabedObject = null;
            }

        }
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
    }

    //Collisions
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) GrabedObject = other.gameObject;
    }

    void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) GrabedObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) GrabedObject = null;
    }
}
