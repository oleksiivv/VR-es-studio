using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerController : NetworkBehaviour
{
    public Vector3 defaultPosition;
    
    public NetworkVariable<Vector3> rotation = new NetworkVariable<Vector3>();
    
    public Vector3 oldRotation;
    
    void Start()
    {
        //transform.position = defaultPosition + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
    }
    
    void Update()
    {
        if(IsServer)
        {
            UpdateServer();
        }
        if(IsClient && IsOwner)
        {
            UpdateClient();
        }
    }
    
    private void UpdateServer()
    {
        //Debug.Log(rotation.Value.y);
        gameObject.transform.eulerAngles = rotation.Value;
        //gameObject.transform.GetChild(0).transform.eulerAngles = rotation.Value;
    }
    
    private void UpdateClient()
    {
        var currentRotation = gameObject.transform.eulerAngles;
        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            currentRotation += new Vector3(0,1,0);
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotation -= new Vector3(0,1,0);
        }
        
        if(oldRotation.y != currentRotation.y || oldRotation.x != currentRotation.x || oldRotation.z != currentRotation.z)
        {
            oldRotation = currentRotation;
            UpdateClientRotationServerRpc(currentRotation);
        }
    }

    [ServerRpc]
    private void UpdateClientRotationServerRpc(Vector3 currentRotation)
    {
        rotation.Value = currentRotation;
    }
}
