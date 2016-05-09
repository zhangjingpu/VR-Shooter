using UnityEngine;
using System.Collections;

<<<<<<< HEAD
=======
/*
    Author: Tung Nguyen
    Purpose: Script for equippable items
    
    Last Edit: N/A
    Reason: N/A
    
    TODO:
        - N/A 
*/


>>>>>>> origin/master
//Attach to the equipable object
//Object has to stay solid
public class EquipObject : MonoBehaviour
{
    //Has to be the same ID as the inventory slot for it to be equiped to the slot (used for defining different types of slots ie: sword slot, gun slot, etc)
    public int SlotID = 0;
    [HideInInspector]
<<<<<<< HEAD
    //Is it equiped to the inventory slot?
=======
>>>>>>> origin/master
    public bool isEquiped = false;

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
