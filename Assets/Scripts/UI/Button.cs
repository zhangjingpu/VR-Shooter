using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

//Affects the actual level
public class Button : MonoBehaviour
{
    //If the script can be ran
    public bool IsActive = false;
    //Get functions to call
    public UnityEvent OnPressedDown;
    public UnityEvent OnHeld;
    public UnityEvent OnPressedUp;
    //If button is pressed
    public bool IsPressedDown = false;
    public bool IsPressedHeld = false;
    public bool IsPressedUp = false;
    //When button is not pushed
    //private Color NormalColor;
    private Vector3 NormalScale;
    //When button is pushed
    public Color HoverColor;
    public Vector3 HoverScale;
    //Speed of which it scales
    public float ShrinkSpeed = 0.1f;
    public float ExpandSpeed = 0.1f;
    //This button's UI Sub Manager Category, set by the Sub Manager
    [HideInInspector]
    public GameObject Parent;
    //Optional
    public GameObject Child;
    //private Color ChildNormalColor;
    private Vector3 ChildNormalScale;
    public Color ChildHoverColor;
    public Vector3 ChildHoverScale;
    public float ChildShrinkSpeed = 0.1f;
    public float ChildExpandSpeed = 0.1f;

    //Override
    [HideInInspector]
    public bool StopPressIdle = false;
    [HideInInspector]
    public bool StopPressDown = false;
    [HideInInspector]
    public bool StopPressHold = false;
    [HideInInspector]
    public bool StopPressUp = false;

    //Reset Values
    private Vector3 ResetPosition;
    private Quaternion ResetRotation;
    private Vector3 ResetScale;
    private Color ResetColor;

    private Vector3 ResetChildPosition;
    private Quaternion ResetChildRotation;
    private Vector3 ResetChildScale;
    private Color ResetChildColor;

    //Access to global variables
    private GlobalVars Global;

    void Start()
    {
        Global = FindObjectOfType<GlobalVars>();

        //if (GetComponent<SpriteRenderer>() != null) NormalColor = GetComponent<SpriteRenderer>().color;
        //else if (GetComponent<TextMesh>() != null) NormalColor = GetComponent<TextMesh>().color;
        NormalScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, transform.localScale.z);
        //Assign reset values
        ResetPosition = transform.localPosition;
        ResetRotation = transform.localRotation;
        ResetScale = transform.localScale;
        //To Do: Support canvas components (Only supports 3d for now)
        if(GetComponent<SpriteRenderer>() != null) ResetColor = GetComponent<SpriteRenderer>().color;
        else if (GetComponent<TextMesh>() != null) ResetColor = GetComponent<TextMesh>().color;
        if (Child != null)
        {
            ChildNormalScale = Child.transform.localScale;
            Child.transform.localScale = new Vector3(0, 0, Child.transform.localScale.z);

            ResetChildPosition = Child.transform.localPosition;
            ResetChildRotation = Child.transform.localRotation;
            ResetChildScale = Child.transform.localScale;
            //To Do: Support canvas components (Only supports 3d for now)
            if (Child.GetComponent<SpriteRenderer>() != null) ResetChildColor = Child.GetComponent<SpriteRenderer>().color;
            else if (Child.GetComponent<TextMesh>() != null) ResetChildColor = Child.GetComponent<TextMesh>().color;
        }
    }

    void Update()
    {
        //If button just woke but was not pressed yet
        if (IsActive == true && IsPressedDown == false && IsPressedUp == false && IsPressedHeld == false)
        {
            if (StopPressIdle == false)
            {
                bool finishedObject = false;
                bool finishedChildObject = false;

                if(ExpandScale(NormalScale) == true) finishedObject = true;
                if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().color = ResetColor;
                else if (GetComponent<TextMesh>() != null) GetComponent<TextMesh>().color = ResetColor;
                if (Child != null)
                {
                    if (ExpandChildScale(ChildNormalScale) == true) finishedChildObject = true;
                    if (Child.GetComponent<SpriteRenderer>() != null) Child.GetComponent<SpriteRenderer>().color = ResetChildColor;
                    else if (Child.GetComponent<TextMesh>() != null) Child.GetComponent<TextMesh>().color = ResetChildColor;
                }
                else finishedChildObject = true;
                if (finishedObject == true && finishedChildObject == true) StopPressIdle = true;
            }
        }
        else StopPressIdle = false;

        //If button was pressed down
        if (IsActive == true && IsPressedDown == true)
        {
            if (StopPressDown == false)
            {
                bool finishedObject = false;
                bool finishedChildObject = false;

                if(ExpandScale(NormalScale + HoverScale) == true) finishedObject = true;
                if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().color = HoverColor;
                else if (GetComponent<TextMesh>() != null) GetComponent<TextMesh>().color = HoverColor;
                if (Child != null)
                {
                    if(ExpandChildScale(ChildNormalScale + ChildHoverScale) == true) finishedChildObject = true;
                    if (Child.GetComponent<SpriteRenderer>() != null) Child.GetComponent<SpriteRenderer>().color = ChildHoverColor;
                    else if (Child.GetComponent<TextMesh>() != null) Child.GetComponent<TextMesh>().color = ChildHoverColor;
                }
                else finishedChildObject = true;
                if (finishedObject == true && finishedChildObject == true) StopPressDown = true;
            }
            OnPressedDown.Invoke();
        }
        else StopPressDown = false;

        //If button is held down
        if (IsActive == true && IsPressedHeld == true) OnHeld.Invoke();

        //If button was pressed up
        if (IsActive == true && IsPressedUp == true)
        {
            if (StopPressUp == false)
            {
                bool finishedObject = false;
                bool finishedChildObject = false;

                if(ShrinkScale(NormalScale) == true) finishedObject = true;
                if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().color = ResetColor;
                else if (GetComponent<TextMesh>() != null) GetComponent<TextMesh>().color = ResetColor;
                if (Child != null)
                {
                    if(ShrinkChildScale(ChildNormalScale) == true) finishedChildObject = true;
                    if (Child.GetComponent<SpriteRenderer>() != null) Child.GetComponent<SpriteRenderer>().color = ResetChildColor;
                    else if (Child.GetComponent<TextMesh>() != null) Child.GetComponent<TextMesh>().color = ResetChildColor;
                }
                else finishedChildObject = true;
                if (finishedObject == true && finishedChildObject == true) StopPressUp = true;
            }
            OnPressedUp.Invoke();
        }
        else StopPressUp = false;
    }

    //Go to the selected level
    public void GoToSelectedLevel(string NextLevel)
    {
        SceneManager.LoadScene(NextLevel);
        IsPressedUp = false;
    }

    //Fade to black and then switch level
    public void FadeToLevel(string NextLevel)
    {
        IsPressedUp = false;
    }

    public void MenuBack()
    {
        //Reset all parent's child
        if (Parent.GetComponent<UISubManager>() != null) Parent.GetComponent<UISubManager>().ResetAllPressed = true;

        //Parent's parent's child
        if (Parent.GetComponent<Button>().Parent.GetComponent<UIManager>() != null)
        {
            for (int i = 0; i < Parent.GetComponent<Button>().Parent.GetComponent<UIManager>().UIManaged.Length; ++i)
            {
                Parent.GetComponent<Button>().Parent.GetComponent<UIManager>().UIManaged[i].GetComponent<Button>().IsActive = true;
            }
        }
        else
        {
            for (int i = 0; i < Parent.GetComponent<Button>().Parent.GetComponent<UISubManager>().UIManaged.Length; ++i)
            {
                Parent.GetComponent<Button>().Parent.GetComponent<UISubManager>().UIManaged[i].GetComponent<Button>().IsActive = true;
            }
        }
        IsPressedUp = false;
    }

    //Reload level
    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        IsPressedUp = false;
    }

    //Exit game
    public void ExitGame()
    {
        Application.Quit();
        IsPressedUp = false;
    }

    //Reset the button
    public bool Reset()
    {
        bool FinishedScalingObject = false;
        bool FinishedScalingChild = false;
        IsPressedDown = false;
        IsPressedHeld = false;
        IsPressedUp = false;

        //Assign reset values
        transform.localPosition = ResetPosition;
        transform.localRotation = ResetRotation;
        //transform.localScale = ResetScale;
        if (ShrinkScale(ResetScale) == true) FinishedScalingObject = true;
        if(GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().color = ResetColor;
        else if (GetComponent<TextMesh>() != null) GetComponent<TextMesh>().color = ResetColor;
        if (Child != null)
        {
            Child.transform.localPosition = ResetChildPosition;
            Child.transform.localRotation = ResetChildRotation;
            //Child.transform.localScale = ResetChildScale;
            if (ShrinkChildScale(ResetChildScale) == true) FinishedScalingChild = true;
            if(Child.GetComponent<SpriteRenderer>() != null) Child.GetComponent<SpriteRenderer>().color = ResetChildColor;
            else if (Child.GetComponent<TextMesh>() != null) Child.GetComponent<TextMesh>().color = ResetChildColor;
        }
        else FinishedScalingChild = true;
        IsActive = false;
        if (FinishedScalingObject == true && FinishedScalingChild == true) return true;
        else return false;
    }

    public bool ShrinkScale(Vector3 Goal)
    {
        if (transform.localScale.x >= Goal.x + 0.02f) transform.localScale -= new Vector3(ShrinkSpeed * Time.deltaTime, 0, 0);
        if (transform.localScale.y >= Goal.y + 0.02f) transform.localScale -= new Vector3(0, ShrinkSpeed * Time.deltaTime, 0);

        //Fix
        bool FinishedX = false;
        bool FinishedY = false;
        if (transform.localScale.x < Goal.x + 0.02f)
        {
            transform.localScale = new Vector3(Goal.x, transform.localScale.y, transform.localScale.z);
            FinishedX = true;
        }
        if (transform.localScale.y < Goal.y + 0.02f)
        {
            transform.localScale = new Vector3(transform.localScale.x, Goal.y, transform.localScale.z);
            FinishedY = true;
        }

        if (FinishedX == true && FinishedY == true) return true;
        else return false;
    }

    public bool ExpandScale(Vector3 Goal)
    {
        if (transform.localScale.x <= Goal.x - 0.02f) transform.localScale += new Vector3(ExpandSpeed * Time.deltaTime, 0, 0);
        if (transform.localScale.y <= Goal.y - 0.02f) transform.localScale += new Vector3(0, ExpandSpeed * Time.deltaTime, 0);

        //Fix
        bool FinishedX = false;
        bool FinishedY = false;
        if (transform.localScale.x > Goal.x - 0.02f)
        {
            transform.localScale = new Vector3(Goal.x, transform.localScale.y, transform.localScale.z);
            FinishedX = true;
        }
        if (transform.localScale.y > Goal.y - 0.02f)
        {
            transform.localScale = new Vector3(transform.localScale.x, Goal.y, transform.localScale.z);
            FinishedY = true;
        }

        if (FinishedX == true && FinishedY == true) return true;
        else return false;
    }

    public bool ShrinkChildScale(Vector3 Goal)
    {
        if (Child != null)
        {
            if (Child.transform.localScale.x >= Goal.x + 0.02f) Child.transform.localScale -= new Vector3(ChildShrinkSpeed * Time.deltaTime, 0, 0);
            if (Child.transform.localScale.y >= Goal.y + 0.02f) Child.transform.localScale -= new Vector3(0, ChildShrinkSpeed * Time.deltaTime, 0);

            //Fix
            bool FinishedX = false;
            bool FinishedY = false;
            if (Child.transform.localScale.x < Goal.x + 0.02f)
            {
                Child.transform.localScale = new Vector3(Goal.x, Child.transform.localScale.y, Child.transform.localScale.z);
                FinishedX = true;
            }
            if (Child.transform.localScale.y < Goal.y + 0.02f)
            {
                Child.transform.localScale = new Vector3(Child.transform.localScale.x, Goal.y, Child.transform.localScale.z);
                FinishedY = true;
            }

            if (FinishedX == true && FinishedY == true) return true;
            else return false;
        }
        else return true;
    }

    public bool ExpandChildScale(Vector3 Goal)
    {
        if (Child != null)
        {
            if (Child.transform.localScale.x <= Goal.x - 0.02f) Child.transform.localScale += new Vector3(ChildExpandSpeed * Time.deltaTime, 0, 0);
            if (Child.transform.localScale.y <= Goal.y - 0.02f) Child.transform.localScale += new Vector3(0, ChildExpandSpeed * Time.deltaTime, 0);

            //Fix
            bool FinishedX = false;
            bool FinishedY = false;
            if (Child.transform.localScale.x > Goal.x - 0.02f)
            {
                Child.transform.localScale = new Vector3(Goal.x, Child.transform.localScale.y, Child.transform.localScale.z);
                FinishedX = true;
            }
            if (Child.transform.localScale.y > Goal.y - 0.02f)
            {
                Child.transform.localScale = new Vector3(Child.transform.localScale.x, Goal.y, Child.transform.localScale.z);
                FinishedY = true;
            }

            if (FinishedX == true && FinishedY == true) return true;
            else return false;
        }
        else return true;
    }

    //Collisions
    void OnTriggerEnter(Collider other)
    {
        //If the right controller touches the button
        //To do: Add collision points on fingers and use finger's names instead so that the buttons dont activate by accedent if the fingers didnt touch it
        if (other.name == "Controller (right)" && IsActive == true)
        {
            IsPressedDown = true;
            IsPressedUp = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Controller (right)" && IsActive == true)
        {
            IsPressedHeld = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Controller (right)" && IsActive == true)
        {
            IsPressedDown = false;
            IsPressedHeld = false;
            IsPressedUp = true;
        }
    }
}
