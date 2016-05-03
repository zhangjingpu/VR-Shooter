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
