using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public List<GameObject> prephabs;
    
    float secondsInterval = 2.5f;
    
    public Vector3 position;
    
    public ScoreController scoreController;
    
    void Start()
    {
        StartCoroutine(Spawn());
    }
    
    IEnumerator Spawn()
    {
        while(true)
        {
            secondsInterval -= 0.01f;
            
            var prephab = prephabs[Random.Range(0, prephabs.Count)];
            
            var createdObject = Instantiate(prephab, position + new Vector3(Random.Range(-1, 1), 0, 0), prephab.transform.rotation) as GameObject;
            
            createdObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 8/5f, ForceMode.Impulse);
            createdObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 0.65f, ForceMode.Impulse);
            createdObject.GetComponent<ObjectToDestroy>().scoreController = scoreController;
            
            yield return new WaitForSeconds(secondsInterval > 0.5f ? secondsInterval : 0.5f);
        }
    }
}
