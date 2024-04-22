using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class EditorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
            GetComponent<TrackedPoseDriver>().enabled = false;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
