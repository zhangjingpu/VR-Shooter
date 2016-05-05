using UnityEngine;
using System.Collections;

public class GunController : UseableObject
{
    public GameObject projectile;
    public Vector3 projectileSpeed;

    private ActivaterButton s_Activator = ActivaterButton.TRIGGER;
    private Rigidbody me;
    private float t_CoolDown;
    private bool s_CanFire;

    //Used in place of a #define.
    private float resetCoolDown() { return 0.5f; }

    private void Start()
    {
        t_CoolDown = 0.5f;
        s_CanFire = false;
    }

    private void Update()
    {
        if(s_CanFire && isActivtorPressed(s_Activator))
        {
            //do the thing
        }
    }


}
