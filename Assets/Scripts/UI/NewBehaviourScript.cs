using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    public StickController leftStick;
    private GameObject minMap;
    private bool s_Current = false;
	// Use this for initialization
	void Start ()
    {
        leftStick = leftStick.GetComponent<StickController>();
        minMap = GetComponent<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(leftStick.Controller.GetPress(leftStick.MenuButton))
        {
            switch (s_Current)
            {
                case true:
                    s_Current = false;
                    break;
                case false:
                    s_Current = true;
                    break;
            }

            minMap.SetActive(s_Current);
        }
	}
}
