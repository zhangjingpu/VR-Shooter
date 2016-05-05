//using UnityEngine;
//using System.Collections;

////Attach this to items that is grabable
//public class GrabableItem1 : MonoBehaviour
//{
//    private bool CurrentlyInteracting;

//    //Physics
//    private Vector3 MovePosition;
//    private Quaternion MoveRotation;
//    private float Angle;
//    private Vector3 Axis;
//    private float velocityMultiplier = 4000f;
//    private float RotationMultiplier = 100f;

//    private Transform InteractionPoint;

//    //Access to the StickController Script
//    private StickController AttachedStick;
//    [HideInInspector]
//    public Rigidbody TheRigidbody;

//    void Start()
//    {
//        TheRigidbody = GetComponent<Rigidbody>();
//        //Creates a new transform
//        InteractionPoint = new GameObject().transform;
//        velocityMultiplier /= TheRigidbody.mass;
//    }

//    //To do: use FixedUpdate
//    void Update()
//    {
//        //Apply physics
//        if(AttachedStick != null && CurrentlyInteracting == true)
//        {
//            //Position
//            MovePosition = AttachedStick.transform.position - InteractionPoint.position;
//            TheRigidbody.velocity = MovePosition * velocityMultiplier * Time.fixedDeltaTime;

//            //Rotation
//            MoveRotation = AttachedStick.transform.rotation * Quaternion.Inverse(InteractionPoint.rotation);
//            MoveRotation.ToAngleAxis(out Angle, out Axis);
//            if (Angle > 180) Angle -= 360;
//            TheRigidbody.angularVelocity = (Time.fixedDeltaTime * Angle * Axis) * RotationMultiplier;
//        }
//    }

//    //Start of a grab
//    public void BeginInteraction(StickController Stick)
//    {
//        AttachedStick = Stick;
//        //Copies the stick's transform to the InteractionPoint
//        InteractionPoint.position = Stick.transform.position;
//        InteractionPoint.rotation = Stick.transform.rotation;
//        //keeps child to self
//        InteractionPoint.SetParent(transform, true);

//        CurrentlyInteracting = true;
//    }

//    //Cancels grab
//    public void EndInteraction(StickController Stick)
//    {
//        //Only be true if it's the stick thats currently holding the item and not the one that isnt
//        if(Stick == AttachedStick)
//        {
//            AttachedStick = null;
//            CurrentlyInteracting = false;
//        }
//    }

//    public bool IsInteracting()
//    {
//        return CurrentlyInteracting;
//    }
//}
