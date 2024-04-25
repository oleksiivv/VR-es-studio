using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class PlayerHud : NetworkBehaviour
{
    private NetworkVariable<NetworkString> playersName = new NetworkVariable<NetworkString>();
    
    private bool overlaySet = false;
    
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            playersName.Value = "Player " + OwnerClientId.ToString();
        }else
        {
            //setOverlay();
            //StartCoroutine(InitXR());
        }
        
        if(!IsOwner)
        {
            //Debug.Log(gameObject.transform.GetChild(0).GetChild(1).parent.name);
            //gameObject.transform.GetChild(0).GetChild(1).parent = transform;
            //Debug.Log(gameObject.transform.GetChild(1).parent.name);
            
            gameObject.GetComponent<Camera>().enabled = false;
            gameObject.GetComponent<TrackedPoseDriver>().enabled = false;
            gameObject.GetComponent<FlareLayer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    
    public void setOverlay()
    {
      //  var trackedPoseDriver = gameObject.GetComponentInChildren<TrackedPoseDriver>();
      //  trackedPoseDriver.SetPoseSource(TrackedPoseDriver.DeviceType.GenericXRDevice, TrackedPoseDriver.TrackedPose.Head);
    }
    
    public IEnumerator InitXR()
    {
        yield return  XRGeneralSettings.Instance.Manager.InitializeLoader();
    }
}