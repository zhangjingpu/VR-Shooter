using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Button))]

//Manages all UIs for this object's category (IE: Main menu category would only affect objects related to the main menu such as title, play button, exit buttons, background, etc)
public class UISubManager : MonoBehaviour
{
    public bool ButtonPressed = false;
    public bool ResetAllPressed = false;

    //The UIs that this object controls
    public GameObject[] UIManaged;

    //Access to local Button
    private Button TheButton;
    //Access to global variables
    private GlobalVars Global;

    void Start()
    {
        Global = FindObjectOfType<GlobalVars>();
        TheButton = GetComponent<Button>();
        for (int i = 0; i < UIManaged.Length; ++i) UIManaged[i].GetComponent<Button>().Parent = gameObject;
    }

    void Update()
    {
        //Shrink object and siblings and move onto next category
        if (ButtonPressed == true)
        {
            if (Reset() == true)
            {
                for (int i = 0; i < UIManaged.Length; ++i) UIManaged[i].GetComponent<Button>().IsActive = true;
                ButtonPressed = false;
            }
        }

        if (ResetAllPressed == true)
        {
            if (AllReset() == true) ResetAllPressed = false;
        }
    }

    public void NextCategory()
    {
        ButtonPressed = true;
    }

    public bool Reset()
    {
        int numberOfSuccessfulReset = 0;
        int manageLength = 0;
        if (GetComponent<Button>().Parent.GetComponent<UIManager>() != null)
        {
            //Access to parent's child list
            for(int i = 0; i < GetComponent<Button>().Parent.GetComponent<UIManager>().UIManaged.Length; ++i)
            {
                if(GetComponent<Button>().Parent.GetComponent<UIManager>().UIManaged[i] != gameObject)
                {
                    if (GetComponent<Button>().Parent.GetComponent<UIManager>().UIManaged[i].GetComponent<UISubManager>().ResetSelf() == true) numberOfSuccessfulReset += 1;
                    else numberOfSuccessfulReset = 0;
                }
            }
            manageLength = GetComponent<Button>().Parent.GetComponent<UIManager>().UIManaged.Length;
        }
        else
        {
            for (int i = 0; i < GetComponent<Button>().Parent.GetComponent<UISubManager>().UIManaged.Length; ++i)
            {
                if (GetComponent<Button>().Parent.GetComponent<UISubManager>().UIManaged[i] != gameObject)
                {
                    if (GetComponent<Button>().Parent.GetComponent<UISubManager>().UIManaged[i].GetComponent<UISubManager>().ResetSelf() == true) numberOfSuccessfulReset += 1;
                    else numberOfSuccessfulReset = 0;
                }
            }
            manageLength = GetComponent<Button>().Parent.GetComponent<UISubManager>().UIManaged.Length;
        }
        if (TheButton.Reset() == true && numberOfSuccessfulReset == manageLength - 1) return true;
        else return false;
    }

    public bool ResetSelf()
    {
        if (TheButton.Reset() == true) return true;
        else return false;

    }

    //Reset all childs
    public bool AllReset()
    {
        int numberOfSuccessfulReset = 0;
        for (int i = 0; i < UIManaged.Length; ++i)
        {
            if (UIManaged[i].GetComponent<Button>().Reset() == true) numberOfSuccessfulReset += 1;
            else numberOfSuccessfulReset = 0;
        }

        if (numberOfSuccessfulReset == UIManaged.Length) return true;
        else return false;
    }
}
