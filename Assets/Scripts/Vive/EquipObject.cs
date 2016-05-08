using UnityEngine;
using System.Collections;

/*
    Author: Tung Nguyen
    Purpose: Script for equippable items
    
    Last Edit: N/A
    Reason: N/A
    
    TODO:
        - N/A 
*/


//Attach to the equipable object
//Object has to stay solid
public class EquipObject : MonoBehaviour
{
    //Has to be the same ID as the inventory slot for it to be equiped to the slot (used for defining different types of slots ie: sword slot, gun slot, etc)
    public int SlotID = 0;
    [HideInInspector]
    public bool isEquiped = false;

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
