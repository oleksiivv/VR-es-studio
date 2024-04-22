using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClientButtonController : MonoBehaviour
{
    public NetworkUIManager networkUIManager;
    
    public void OnPointerEnter()
    {
        networkUIManager.StartClient();
    }
}
