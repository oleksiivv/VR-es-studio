using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NPCController : NetworkBehaviour
{
    public GameObject npc;

    public GameObject npcMock;

    public int npcsNumber = 5;

    public Vector2 xPosRange, zPosRange;

    public void Spawn()
    {
        if(IsServer)
        {
            Debug.Log("Start npc spawn");
            for(int i=0; i<npcsNumber; i++)
            {
                Instantiate(npc, new Vector3(Random.Range(100, 150),0.5f, Random.Range(100, 150)), npc.transform.rotation);
            }

            for(int i=0; i<1; i++)
            {
                Instantiate(npcMock, new Vector3(Random.Range(-5, 5),0.5f, Random.Range(-5, 5)), npcMock.transform.rotation);
            }
        }
    }
}
