using UnityEngine;
using System.Collections;

//Attach to the ghost slot (the inventory)
//Object has to have triggered check on collider
public class InventorySlot : MonoBehaviour
{
    //Has to be the same ID as the held item for it to be equiped to the slot (used for defining different types of slots ie: sword slot, gun slot, etc)
    public int SlotID = 0;
    public GameObject EquipedItem;

    //Collisions
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            //(The root object only just incase one of the childs collided, we only want the top most parent since thats where all scripts are stored)
            //I HATE NON UNIFORM LOCAL SCALING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Transform root = other.transform.root;
            //If the slot isnt full, item isnt already equiped, and slot id matches
            if (EquipedItem == null && root.GetComponent<EquipObject>().isEquiped == false && root.GetComponent<EquipObject>().SlotID == SlotID)
            {
                //Stops object from moving and freeze it to the slot's position also turns it into a sibling
                root.GetComponent<Rigidbody>().isKinematic = true;
                root.parent = transform.parent;
                root.localPosition = transform.localPosition;
                root.localRotation = transform.localRotation;
                root.GetComponent<EquipObject>().isEquiped = true;
                EquipedItem = root.gameObject;
            }
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
