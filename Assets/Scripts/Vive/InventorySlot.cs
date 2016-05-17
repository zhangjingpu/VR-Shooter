using UnityEngine;
using System.Collections;

//Attach to the ghost slot (the inventory)
//Object has to have triggered check on collider
public class InventorySlot : MonoBehaviour
{
    //Has to be the same ID as the held item for it to be equiped to the slot (used for defining different types of slots ie: sword slot, gun slot, etc)
    public int SlotID = 0;
    public GameObject EquipedItem;

    void Update()
    {
        if(EquipedItem != null)
        {
            if (EquipedItem.GetComponent<EquipObject>().isHeld == true) EquipedItem = null;
        }
    }

    //Collisions
    void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Grabable")) GrabedObject = other.gameObject;
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            //(The root object only just incase one of the childs collided, we only want the top most parent since thats where all scripts are stored)
            //I HATE NON UNIFORM LOCAL SCALING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //If the slot isnt full, item isnt already equiped, and slot id matches
            if (EquipedItem == null && other.GetComponent<EquipObject>() != null)
            {
                if(other.GetComponent<EquipObject>().isEquiped == false && other.GetComponent<EquipObject>().SlotID == SlotID && other.GetComponent<EquipObject>().isHeld == false)
                {
                    //Stops object from moving and freeze it to the slot's position also turns it into a sibling
                    other.GetComponent<EquipObject>().Root.GetComponent<Rigidbody>().isKinematic = true;
                    other.GetComponent<EquipObject>().Root.transform.parent = transform.parent;
                    other.GetComponent<EquipObject>().Root.transform.localPosition = transform.localPosition;
                    other.GetComponent<EquipObject>().Root.transform.localRotation = transform.localRotation;
                    other.GetComponent<EquipObject>().isEquiped = true;
                    EquipedItem = other.gameObject;
                }
            }
        }
    }
}
