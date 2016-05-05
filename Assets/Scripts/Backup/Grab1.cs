//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic; //For List

//public class Grab1 : MonoBehaviour
//{
//    //Maintaining Velocity
//    private Vector3 lastPos;
//    [HideInInspector]
//    public Vector3 vel;

//    //Grab
//    //Incase multiple objects are currently being triggered, used to only grab the closest object
//    private List<GrabableItem> TriggeredObjects = new List<GrabableItem>();
//    private GrabableItem ClosestItem;
//    public GrabableItem CurrentGrabedItem;

//    [HideInInspector]
//    public StickController Stick;

//    void Start ()
//    {
//        Stick = GetComponent<StickController>();
//        lastPos = transform.position;
//    }
	
//	void Update ()
//    {
//        vel = (transform.position - lastPos) / Time.deltaTime;
//        lastPos = transform.position;
//        GrabUpdate();
//    }

//    void GrabUpdate()
//    {
//        //Grab Start
//        if (Stick.Controller.GetPressDown(Stick.GripyButton))
//        {
//            float maxDistance = float.MaxValue;
//            //Find closest grabable object from the list of grabables
//            for (int i = 0; i < TriggeredObjects.Count; ++i)
//            {
//                float distance = Vector3.Distance(TriggeredObjects[i].transform.position, transform.position);
//                if (distance < maxDistance)
//                {
//                    maxDistance = distance;
//                    ClosestItem = TriggeredObjects[i];
//                }
//            }
//            //Gives the closest item
//            CurrentGrabedItem = ClosestItem;
//            //resets the list
//            TriggeredObjects.Clear();

//            //Checks the item if its valid
//            if (CurrentGrabedItem != null)
//            {
//                //If interacting, end current interaction to begin a new one
//                if (CurrentGrabedItem.IsInteracting() == true) CurrentGrabedItem.EndInteraction(Stick);
//                CurrentGrabedItem.BeginInteraction(Stick);
//            }
//        }

//        //End Grab
//        if (Stick.Controller.GetPressUp(Stick.GripyButton) && CurrentGrabedItem != null)
//        {
//            CurrentGrabedItem.GetComponent<Rigidbody>().AddForce(vel * 100);
//            CurrentGrabedItem.EndInteraction(Stick);
//            print("Moo");
//        }
//    }

//    //Collisions
//    void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
//        {
//            //Add to list of grabable items
//            if (other.GetComponent<GrabableItem>() != null) TriggeredObjects.Add(other.GetComponent<GrabableItem>());
//            print("Poop");
//        }
//    }

//    void OnTriggerStay(Collider other)
//    {

//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
//        {
//            //remove from list of grabable items
//            if (other.GetComponent<GrabableItem>() != null) TriggeredObjects.Remove(other.GetComponent<GrabableItem>());
//            print("bum bum bummmmm");
//        }
//    }
//}
