using UnityEngine;
using System.Collections;

//Manages all UISubManagers
public class UIManager : MonoBehaviour
{
    //The UIs that this object controls
    public GameObject[] UIManaged;

    //Access to global variables
    private GlobalVars Global;

    //What the UI should be looking at (So that the UI dont look flat from the side)
    public GameObject LookAtTarget;

    void Start ()
    {
        //Shrink and move onto next category, also links parent
        Global = FindObjectOfType<GlobalVars>();
        for (int i = 0; i < UIManaged.Length; ++i)
        {
            UIManaged[i].GetComponent<Button>().Parent = gameObject;
            UIManaged[i].GetComponent<Button>().IsActive = true;
        }
    }

    void Update()
    {
        //To do: get angle and limit the look
        if(LookAtTarget != null) transform.LookAt(LookAtTarget.transform);
    }
}
