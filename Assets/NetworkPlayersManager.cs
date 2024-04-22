using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayersManager : NetworkBehaviour
{
    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();
    
    public int PlayersInGame
    {
        get
        {
            return playersInGame.Value;
        }
    }
    
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if(IsServer)
            {
                Debug.Log("Connected: "+id.ToString());
                playersInGame.Value++;
            }
        };
        
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
            {
                Debug.Log("Disonnected: "+id.ToString());
                playersInGame.Value--;
            }
        };
    }
}
