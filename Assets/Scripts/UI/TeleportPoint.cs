using UnityEngine;
using System.Collections;

//device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad;



public class TeleportPoint : MonoBehaviour
{

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerPressed = false;
    public bool triggerPressedDown = false;
    public bool triggerPressedUp = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public GameObject startGameobject;
    public GameObject rig;
    public Vector3 startingTeleportDot;
    public Vector3 endingTeleportDot;
    public float blinkTransitionSpeed = 0.6f;

    // Use this for initialization
    void Start ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(controller == null)
        {
            Debug.Log("Controller is not plugged in");
            return;
        }

        triggerPressedDown = controller.GetPressDown(triggerButton);
        triggerPressedUp = controller.GetPressUp(triggerButton);
        triggerPressed = controller.GetPress(triggerButton);

        if(triggerPressedDown)
        {
            startingTeleportDot = transform.position;
            Debug.Log("Trigger is held down");
        }
        if (triggerPressedUp)
        {
            endingTeleportDot = transform.position;
            float distance = Vector3.Distance(startingTeleportDot, endingTeleportDot);
            //print("distance: " + distance);
            Debug.Log("Trigger trigger was let go");
            if (startingTeleportDot.y > endingTeleportDot.y && startingTeleportDot.z < endingTeleportDot.z && distance >= 0.7f)
            {
                startingTeleportDot = Vector3.zero;
                endingTeleportDot = Vector3.zero;
                Teleport();
            }
        }
    }

    protected virtual void Blink()
    {
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, blinkTransitionSpeed);
    }

    void Teleport()
    {
        Blink();
        Ray pointerRaycast = new Ray(startGameobject.transform.position, startGameobject.transform.forward);
        RaycastHit pointerCollidedWith;
        bool rayHit = Physics.Raycast(pointerRaycast, out pointerCollidedWith);
        if (rayHit == true)
        {
            if (pointerCollidedWith.transform.tag == "Teleportable")
            {
                Debug.Log("penis");
                rig.transform.position = new Vector3(pointerCollidedWith.point.x, rig.transform.position.y , pointerCollidedWith.point.z);
            }
        }
    }
}


