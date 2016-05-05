using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For List

public class Grab : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject GrabedObject;

    private Vector3 lastPos;

    [HideInInspector]
    public Vector3 vel;
    [HideInInspector]
    public StickController Stick;

    void Start()
=======
    //Maintaining Velocity
    private Vector3 lastPos;
    [HideInInspector]
    public Vector3 vel;

    //Grab
    //Incase multiple objects are currently being triggered, used to only grab the closest object
    private List<GrabableItem> TriggeredObjects = new List<GrabableItem>();
    private GrabableItem ClosestItem;
    public GrabableItem CurrentGrabedItem;

    [HideInInspector]
    public StickController Stick;

    void Start ()
>>>>>>> 1a31a26734b0c482936bdbe9f30a00e0302c17b5
    {
        Stick = GetComponent<StickController>();
        lastPos = transform.position;
    }
<<<<<<< HEAD

    void Update()
    {

        vel = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;

        UpdateGrab();
    }

    void UpdateGrab()
    {
        if (GrabedObject != null)
        {
            if (Stick.Controller.GetPress(Stick.GripyButton) && GrabedObject != null)
            {
                if (GrabedObject.GetComponent<Rigidbody>() != null)
                {
                    GrabedObject.GetComponent<Rigidbody>().isKinematic = true;
                    GrabedObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    GrabedObject.GetComponent<Rigidbody>().useGravity = false;
                }
                GrabedObject.transform.parent = transform; //Turns to a child also
            }
            else
            {
                GrabedObject.transform.SetParent(null);
                GrabedObject.GetComponent<Rigidbody>().isKinematic = false;
                GrabedObject.GetComponent<Rigidbody>().AddForce(vel * 100);
                GrabedObject = null;
            }

=======
	
	void Update ()
    {
        vel = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
        GrabUpdate();
    }

    void GrabUpdate()
    {
        //Grab Start
        if (Stick.Controller.GetPressDown(Stick.GripyButton))
        {
            float maxDistance = float.MaxValue;
            //Find closest grabable object from the list of grabables
            for (int i = 0; i < TriggeredObjects.Count; ++i)
            {
                float distance = Vector3.Distance(TriggeredObjects[i].transform.position, transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    ClosestItem = TriggeredObjects[i];
                }
            }
            //Gives the closest item
            CurrentGrabedItem = ClosestItem;
            //resets the list
            TriggeredObjects.Clear();

            //Checks the item if its valid
            if (CurrentGrabedItem != null)
            {
                //If interacting, end current interaction to begin a new one
                if (CurrentGrabedItem.IsInteracting() == true) CurrentGrabedItem.EndInteraction(Stick);
                CurrentGrabedItem.BeginInteraction(Stick);
            }
        }

        //End Grab
        if (Stick.Controller.GetPressUp(Stick.GripyButton) && CurrentGrabedItem != null)
        {
            CurrentGrabedItem.GetComponent<Rigidbody>().AddForce(vel * 100);
            CurrentGrabedItem.EndInteraction(Stick);
>>>>>>> 1a31a26734b0c482936bdbe9f30a00e0302c17b5
        }
    }

    //Collisions
    void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) GrabedObject = other.gameObject;
=======
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            //Add to list of grabable items
            if (other.GetComponent<GrabableItem>() != null) TriggeredObjects.Add(other.GetComponent<GrabableItem>());
            print("Poop");
        }
>>>>>>> 1a31a26734b0c482936bdbe9f30a00e0302c17b5
    }

    void OnTriggerStay(Collider other)
    {
<<<<<<< HEAD
        //if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) GrabedObject = other.gameObject;
=======

>>>>>>> 1a31a26734b0c482936bdbe9f30a00e0302c17b5
    }

    void OnTriggerExit(Collider other)
    {
<<<<<<< HEAD
        //if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) GrabedObject = null;
=======
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            //remove from list of grabable items
            if (other.GetComponent<GrabableItem>() != null) TriggeredObjects.Remove(other.GetComponent<GrabableItem>());
            print("Poop");
        }
>>>>>>> 1a31a26734b0c482936bdbe9f30a00e0302c17b5
    }
}
