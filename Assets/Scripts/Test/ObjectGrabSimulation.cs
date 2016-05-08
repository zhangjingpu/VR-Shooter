using UnityEngine;
using System.Collections;

/*
    Author: Tung Nguyen
    Purpose: Proxy controllers for testing with out the vive (Gold Star to Tung!!)

    Last Edit: N/A
    Reason: N/A

    TODO:
        -N/A
*/

public class ObjectGrabSimulation : MonoBehaviour
{
    public GameObject TestChildObject;
    public float Smoothness = 0;
    private Vector3 GrabedObjectStart;
    private Vector3 Velo = Vector3.zero;

    void Start()
    {
        GrabedObjectStart = TestChildObject.transform.localPosition;
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        Smoothness = Mathf.Clamp01(Smoothness);
        Vector3 Mod = Vector3.SmoothDamp(TestChildObject.transform.localPosition, GrabedObjectStart, ref Velo, Smoothness);

        TestChildObject.GetComponent<Rigidbody>().useGravity = false;
        TestChildObject.GetComponent<Rigidbody>().velocity = Velo;
        TestChildObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        TestChildObject.transform.rotation = transform.rotation;
    }
}
