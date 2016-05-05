using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For List

public class Grab : MonoBehaviour
{
    public GameObject GrabedObject;

    private Vector3 lastPos;

    [HideInInspector]
    public Vector3 vel;
    [HideInInspector]
    public StickController Stick;

    void Start()
    {
        Stick = GetComponent<StickController>();
        lastPos = transform.position;
    }

    void Update()
    {
        //Tracks the velocity in which will be applied to the throw
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
                GrabedObject.GetComponent<Rigidbody>().isKinematic = true;
                //GrabedObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                //GrabedObject.GetComponent<Rigidbody>().useGravity = false;
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
