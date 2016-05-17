using UnityEngine;
using System.Collections;

//Attach to the equipable object
//Object has to stay solid
public class EquipObject : MonoBehaviour
{
    //Has to be the same ID as the inventory slot for it to be equiped to the slot (used for defining different types of slots ie: sword slot, gun slot, etc)
    public int SlotID = 0;
    [HideInInspector]
    //Is it equiped to the inventory slot?
    public bool isEquiped = false;
    [HideInInspector]
    //is this object currently grabbed?
    public bool isHeld = false;
    [HideInInspector]
    public GameObject Root;

    void Start()
    {
        Root = transform.root.gameObject;
    }
}
