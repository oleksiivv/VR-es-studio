using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-3, 3), 8, Random.Range(-3, 3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
