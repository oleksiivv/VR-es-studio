using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class NetworkPlayerPositionController : NetworkBehaviour
{
    public Vector3 defaultPosition;

    public NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();

    public Vector3 oldPosition;

    public GameObject head;

    public ThirdPersonController character;

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
        //gameObject.transform.eulerAngles = rotation.Value;
        gameObject.transform.position = position.Value;
        //gameObject.transform.GetChild(0).transform.eulerAngles = rotation.Value;
    }

    private void UpdateClient()
    {
        var currentPosition = gameObject.transform.position;

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.eulerAngles = head.transform.eulerAngles;
            //gameObject.transform.Translate(Vector3.back / 5);
            character.Move(gameObject);
            currentPosition = gameObject.transform.position;
        }else
        {
            character.Stop();
        }

        if(oldPosition.y != currentPosition.y || oldPosition.x != currentPosition.x || oldPosition.z != currentPosition.z)
        {
            oldPosition = currentPosition;
            character.isMove=true;
            UpdateClientPositionServerRpc(currentPosition);
        }
    }

    [ServerRpc]
    private void UpdateClientPositionServerRpc(Vector3 currentPosition)
    {
        position.Value = currentPosition;
    }
}
