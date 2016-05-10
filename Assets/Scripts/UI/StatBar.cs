using UnityEngine;
using System.Collections;

//Controlls anything involving bars(Health bars, energy bars, etc)
public class StatBar : MonoBehaviour
{
    private Vector3 StartingScale;
    private Vector3 StartingPosition;
    public bool IsHealthbar = false;
    public bool IsEnergybar = false;

    //Access to global vars
    public GlobalVars Global;

    void Awake()
    {
        StartingScale = transform.localScale;
        StartingPosition = transform.localPosition;

    }

	// Use this for initialization
	void Start ()
    {
        Global = FindObjectOfType<GlobalVars>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //For buttons to be fully scaled before activating
        if (IsHealthbar == true)
        {
            //Get a percentage of health left
            float progress = (Global.CurrentHealth / Global.MaxHealth);
            Vector3 ModifiedScale = new Vector3(StartingScale.x * progress, StartingScale.y, StartingScale.z);
            //Gets the difference in x
            float ModifiedScaleX = StartingScale.x - ModifiedScale.x;
            transform.localScale = ModifiedScale;
            print(ModifiedScaleX);
            transform.localPosition = new Vector3(StartingPosition.x - (ModifiedScaleX / 2), StartingPosition.y, StartingPosition.z);
        }
        else if (IsEnergybar == true)
        {

        }
    }
}
