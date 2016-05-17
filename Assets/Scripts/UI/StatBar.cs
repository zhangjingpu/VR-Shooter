using UnityEngine;
using System.Collections;

//Controlls anything involving bars(Health bars, energy bars, etc)
public class StatBar : MonoBehaviour
{
    public bool UseStaticBarChange = false;
    private Vector3 StartingScale;
    private Vector3 StartingPosition;
    public bool IsHealthbar = false;
    public bool IsEnergybar = false;

    public float FakeProgress = 1;
    private float FakeVelo = 0;
    public float Smoothness = 0.01f;

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
        if (GetComponent<Button>().StopPressIdle == true)
        {
            if (UseStaticBarChange == true)
            {
                //Get a percentage of health left
                float progress = 0;
                if (IsHealthbar == true) progress = (Global.CurrentHealth / Global.MaxHealth);
                if (IsEnergybar == true) progress = (Global.CurrentEnergy / Global.MaxEnergy);
                Vector3 ModifiedScale = new Vector3(StartingScale.x * progress, StartingScale.y, StartingScale.z);
                //Gets the difference in x
                float ModifiedScaleX = StartingScale.x - ModifiedScale.x;
                transform.localScale = ModifiedScale;
                transform.localPosition = new Vector3(StartingPosition.x - (ModifiedScaleX / 2), StartingPosition.y, StartingPosition.z);
            }
            else
            {
                float Realprogress = 0;
                //Get a percentage of health left
                if (IsHealthbar == true) Realprogress = (Global.CurrentHealth / Global.MaxHealth);
                if (IsEnergybar == true) Realprogress = (Global.CurrentEnergy / Global.MaxEnergy);
                FakeProgress = Mathf.SmoothDamp(FakeProgress, Realprogress, ref FakeVelo, Smoothness);
                //if (FakeProgress < Realprogress) FakeProgress = Realprogress;
                Vector3 ModifiedScale = new Vector3(StartingScale.x * FakeProgress, StartingScale.y, StartingScale.z);
                //Gets the difference in x
                float ModifiedScaleX = StartingScale.x - ModifiedScale.x;
                transform.localScale = ModifiedScale;
                transform.localPosition = new Vector3(StartingPosition.x - (ModifiedScaleX / 2), StartingPosition.y, StartingPosition.z);
            }
        }
    }
}
