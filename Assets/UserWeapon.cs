using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserWeapon : MonoBehaviour
{
    public GameObject weaponInUse;

    public GameObject weaponIdle;

    void Start()
    {
        UseWeapon();
    }

    public void Switch()
    {
        if(weaponInUse.activeSelf)
        {
            IdleWeapon();
        }else
        {
            UseWeapon();
        }
    }

    public void UseWeapon()
    {
        weaponInUse.SetActive(true);

        weaponIdle.SetActive(false);

        MakeNPCsInteractable();
    }

    public void UseWeaponAsNPC()
    {
        weaponInUse.SetActive(true);
        weaponIdle.SetActive(false);
    }

    public void IdleWeaponAsNPC()
    {
        weaponInUse.SetActive(false);
        weaponIdle.SetActive(true);
    }

    public void IdleWeapon()
    {
        weaponInUse.SetActive(false);

        weaponIdle.SetActive(true);

        MakeNPCsNonInteractable();
    }

    private void MakeNPCsInteractable()
    {
        foreach (var npc in FindObjectsOfType<NPCMove>())
        {
            //npc.gameObject.name = npc.gameObject.name+"_interactable";
            SetLayerRecursively(npc.gameObject, LayerMask.NameToLayer("Interactive"));
        }
    }

    private void MakeNPCsNonInteractable()
    {
        foreach (var npc in FindObjectsOfType<NPCMove>())
        {
            //npc.gameObject.name = npc.gameObject.name.Replace("_interactable", "");
            SetLayerRecursively(npc.gameObject, LayerMask.NameToLayer("Default"));
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
