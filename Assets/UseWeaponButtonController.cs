using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeaponButtonController : MonoBehaviour
{
    public UserWeapon userWeapon;

    public void OnPointerEnter()
    {
        Debug.Log("WEAPON SWITCH");
        userWeapon.Switch();
    }
}
