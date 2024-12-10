using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class HandleBulletNPC : NetworkBehaviour
{
    public ParticleSystem deathEffect;

    public void OnPointerEnter()
    {
        if(!gameObject.transform.parent.gameObject.name.ToLower().Contains("_interactable"))
        {
            //return;
        }

        GameObject.Find("NetworkManager").GetComponent<SimulationApiFacade>().sendNpcKillResults(NetworkManager.Singleton.LocalClientId.ToString());

        deathEffect.gameObject.transform.parent=null;
        deathEffect.Play();

        gameObject.SetActive(false);
    }
}
