using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using Unity.Services.Core.Environments;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay.Models;

public class RelayManager : MonoBehaviour
{
    public string environment = "production";
    
    public int maxConnections = 10;
    
    public bool IsRelayEnabled = true;
    
    public UnityTransport Transport;
    
    public SimulationApiFacade api;
    
    public int applicationId;
    
    public async Task<RelayHostData> SetupRelay()
    {
        Debug.Log("Relay server starting with max connections:"+maxConnections.ToString());
        
        InitializationOptions options = new InitializationOptions().SetEnvironmentName(environment);
        
        await UnityServices.InitializeAsync(options);
        
        if(!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        
        Allocation allocation = await Relay.Instance.CreateAllocationAsync(maxConnections);
        
        RelayHostData relayHostData = new RelayHostData
        {
            Key = allocation.Key,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            IPv4Address = allocation.RelayServer.IpV4,
            ConnectionData = allocation.ConnectionData
        };
        
        relayHostData.JoinCode = await Relay.Instance.GetJoinCodeAsync(relayHostData.AllocationID);
        
        Transport.SetRelayServerData(relayHostData.IPv4Address, relayHostData.Port, relayHostData.AllocationIDBytes,
            relayHostData.Key, relayHostData.ConnectionData);
        
        Debug.Log("Relay server generated join code:"+relayHostData.JoinCode);
        
        api.StartSimulation(applicationId, relayHostData.JoinCode, relayHostData.AllocationID.ToString());
        
        return relayHostData;
    }
    
    private InputField _joinCodeField;
    private GameObject _initialCamera;
    
   public async Task<bool> JoinRelayNew(InputField joinCodeField, GameObject initialCamera)
    {
        this._joinCodeField = joinCodeField;
        this._initialCamera = initialCamera;

        api.JoinSimulation(this._joinCodeField, applicationId, environment);

        if (!string.IsNullOrEmpty(this._joinCodeField.text))
        {
            JoinOnCodeReceived();
        } else
        {
            Invoke(nameof(JoinOnCodeReceived), 3);
        }

        return true;
    }
    
    public async Task<RelayJoinData> JoinRelay(string joinCode)
    {
        InitializationOptions options = new InitializationOptions().SetEnvironmentName(environment);
        
        await UnityServices.InitializeAsync(options);
        
        if(!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        
        JoinAllocation allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
        
        RelayJoinData relayJoinData = new RelayJoinData
        {
            Key = allocation.Key,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            HostConnectionData = allocation.HostConnectionData,
            IPv4Address = allocation.RelayServer.IpV4,
            JoinCode = joinCode
        };
        
        Transport.SetRelayServerData(relayJoinData.IPv4Address, relayJoinData.Port, relayJoinData.AllocationIDBytes,
            relayJoinData.Key, relayJoinData.ConnectionData, relayJoinData.HostConnectionData);
        
        Debug.Log("Client joined relay server with join code:"+relayJoinData.JoinCode);
        
        return relayJoinData;
    }
    
    private async void JoinOnCodeReceived()
    {
        if (string.IsNullOrEmpty(this._joinCodeField.text))
        {
            Invoke(nameof(JoinOnCodeReceived), 3);
            return;
        }
        
        InitializationOptions options = new InitializationOptions().SetEnvironmentName(environment);

        await UnityServices.InitializeAsync(options);

        if(!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        
        JoinAllocation allocation = await Relay.Instance.JoinAllocationAsync(this._joinCodeField.text);

        RelayJoinData relayJoinData = new RelayJoinData
        {
           Key = allocation.Key,
           Port = (ushort)allocation.RelayServer.Port,
           AllocationID = allocation.AllocationId,
           AllocationIDBytes = allocation.AllocationIdBytes,
           ConnectionData = allocation.ConnectionData,
           HostConnectionData = allocation.HostConnectionData,
           IPv4Address = allocation.RelayServer.IpV4,
           JoinCode = this._joinCodeField.text
        };

        Transport.SetRelayServerData(relayJoinData.IPv4Address, relayJoinData.Port, relayJoinData.AllocationIDBytes,
            relayJoinData.Key, relayJoinData.ConnectionData, relayJoinData.HostConnectionData);

        Debug.Log("Client joined relay server with join code:"+relayJoinData.JoinCode);
        
        Debug.Log("start client");
        
        if(NetworkManager.Singleton.StartClient())
        {
            //
        }
    }
}
