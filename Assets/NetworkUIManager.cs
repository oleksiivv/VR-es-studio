using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkUIManager : MonoBehaviour
{
    public Button startServer;
    
    public Button startHost;
    
    public Button startClient;
    
    public InputField inputJoinCode;
    
    public Text playersInGame;
    
    public NetworkPlayersManager networkPlayersManager;
    
    public RelayManager relayManager;
    
    public GameObject initCamera;
    
    private void Start()
    {
        startHost.onClick.AddListener(async () =>
        {
            StartHost();
        });
        
        startServer.onClick.AddListener(() =>
        {
            StartServer();
        });
        
        startClient.onClick.AddListener(async () =>
        {
            StartClient();
        });
    }
    
    private void Update()
    {
        //todo: remove from update
        playersInGame.text = "Players in game: " + networkPlayersManager.PlayersInGame.ToString();
    }
    
    public async void StartClient()
    {
        if(relayManager.IsRelayEnabled)
        {
            await relayManager.JoinRelayNew(inputJoinCode, initCamera);
        }
        
       
    }
    
    public async void StartHost()
    {
        if(relayManager.IsRelayEnabled)
        {
            await relayManager.SetupRelay();
        }

        Debug.Log("start host");
        if(NetworkManager.Singleton.StartHost())
        {
            //
        }
        
        Destroy(initCamera);
    }
    
    public async void StartServer()
    {
        Debug.Log("start server");
        if(NetworkManager.Singleton.StartServer())
        {
            //
        }

        Destroy(initCamera);
    }
}
