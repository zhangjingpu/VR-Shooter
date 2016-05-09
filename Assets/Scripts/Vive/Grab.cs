using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For List


/*
    Author: Tung Nguyen
    Purpose: Basic grab behavior for picking up and throwing objects.

    Last Edit: A. Connor Adam
    Reason: Small edit to ease intergration into StickController class

    TODO:
        -Debug
        -Integrate into StickController 
*/


//Grab should be integrated as a standard part of the StickController class, just to simplify.
//...But only after it works.
public class Grab : StickController
{
    //The object that is grabed
    public GameObject GrabedObject;

    //Maintaining physics for grabed objects
    public float FollowSmoothness = 0.01f;
    private Vector3 GrabedObjectStart;
    private Vector3 FollowVelocity = Vector3.zero;
    private bool SetOnce = true;

    //Keep track of throw velocity
    private Vector3 lastPos;
    [HideInInspector]
    public Vector3 vel;
<<<<<<< HEAD

    //Get access to the controller
    [HideInInspector]
    public StickController Stick;
=======
>>>>>>> origin/master

    void Start()
    {
        Stick = GetComponent<StickController>();
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        //Tracks the velocity in which will be applied to the throw
<<<<<<< HEAD
        vel = (transform.position - lastPos) / Time.fixedDeltaTime;
=======
        vel = (transform.position - lastPos) / Time.deltaTime;
>>>>>>> origin/master
        lastPos = transform.position;

        UpdateGrab();
    }

    void UpdateGrab()
    {
        if (GrabedObject != null)
        {
<<<<<<< HEAD
            if (Stick.Controller.GetPress(Stick.GripyButton))
=======
            if (isHeld(ActivatorButton.GRIPPY))
>>>>>>> origin/master
            {
                //Turns to a child
                GrabedObject.transform.parent = transform;
                //Set the point of collision at the start of the new child
                if(SetOnce == true)
                {
                    GrabedObjectStart = GrabedObject.transform.localPosition;
                    SetOnce = false;
                }
                //Clamps 0 - 1
                FollowSmoothness = Mathf.Clamp01(FollowSmoothness);
                Vector3 Mod = Vector3.SmoothDamp(GrabedObject.transform.localPosition, GrabedObjectStart, ref FollowVelocity, FollowSmoothness);

                GrabedObject.GetComponent<Rigidbody>().useGravity = false;
                GrabedObject.GetComponent<Rigidbody>().velocity = FollowVelocity;
                GrabedObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                GrabedObject.transform.rotation = transform.rotation;
            }
            else
            {
                GrabedObject.transform.SetParent(null);
                GrabedObject.GetComponent<Rigidbody>().useGravity = true;
                GrabedObject.GetComponent<Rigidbody>().AddForce(vel * 100);
                SetOnce = true;
                GrabedObject = null;
            }

        }
    }

    //Collisions
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            if (other.GetComponent<Rigidbody>() != null) GrabedObject = other.gameObject;
        }
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
