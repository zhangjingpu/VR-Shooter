using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
--------------------------------
Author: Tung Nguyen

Purpose: Like the LevelTracker script in which it stores data for the gameobjects to use except
storing it here will save the data to be used on the next loaded scene also. Many scripts will
rely on this script.

Script communicate with: LevelTracker(For spawning the object this script is attached to, not
directly)

Used by: Almost everything

Last edited: Tung Nguyen
--------------------------------
*/

public class GlobalVars : MonoBehaviour
{
    //Currency
    public int Money = 0;
    public int Scrap = 0;
    public int Wood = 0;
    public int Stone = 0;

    //Stats
    public float CurrentPlatformID = 0; //The current platform id the player is currently controlling
    public float MaxHealth = 100;
    public float CurrentHealth = 100;
    public float MaxShipHealth = 100;
    public float CurrentShipHealth = 100;
    public float Armour = 10;
    public float Damage = 10;

    //For World Gen spawning unique platforms
    public float currentPlatformSpawnID = 0;

    //Misc
    public bool Save = false;
    public bool Load = false;
    public Vector3 CurrentPosition = new Vector3(0, 0, 0);

    //Tests
    public bool DebugMode = false;

    public string MainMenu;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        //Debug Toggle
        if (Input.GetButtonDown("Debug"))
        {
            if (DebugMode == true) DebugMode = false;
            else DebugMode = true;
        }

        //Reload the scene
        if (Input.GetButtonDown("Home")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //Back to main menu
        if (Input.GetButtonDown("Pause"))
        {
            SceneManager.LoadScene(MainMenu);
        }
    }
}
