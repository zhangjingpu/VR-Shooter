using UnityEngine;
using System.Collections;

public class ObjectGrabSimulation : MonoBehaviour
{
    public GameObject TestChildObject;
    public float Smoothness = 0;
    private Vector3 GrabedObjectStart;
    private Vector3 Velo = Vector3.zero;

    void Start()
    {
        GrabedObjectStart = transform.position - TestChildObject.transform.position;
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        Smoothness = Mathf.Clamp01(Smoothness);
        //Vector3 difference = GrabedObjectStart - transform.position;
        Vector3.SmoothDamp(TestChildObject.transform.position, transform.position - GrabedObjectStart, ref Velo, Smoothness);

        TestChildObject.GetComponent<Rigidbody>().useGravity = false;
        TestChildObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        TestChildObject.GetComponent<Rigidbody>().velocity = Velo;
        TestChildObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        TestChildObject.transform.rotation = transform.rotation;
    }
}
