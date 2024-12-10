using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class HandleBullet : NetworkBehaviour
{
    public ParticleSystem deathEffect;

    void Start()
    {
        //Invoke(nameof(OnPointerEnter), 15f);
    }

    public void OnPointerEnter()
    {
        GameObject.Find("NetworkManager").GetComponent<SimulationApiFacade>().sendResults(NetworkManager.Singleton.LocalClientId.ToString());

        deathEffect.gameObject.transform.parent=null;
        deathEffect.Play();

        GameObject.Find("DeathPanel").transform.localScale=new Vector3(1, 1, 1);

        gameObject.SetActive(false);
    }
}
