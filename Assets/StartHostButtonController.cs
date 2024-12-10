using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHostButtonController : MonoBehaviour
{
    public NetworkUIManager networkUIManager;

    public void OnPointerEnter()
    {
        networkUIManager.StartHost();
        gameObject.transform.position += new Vector3(100, 100, 100);
    }
}
