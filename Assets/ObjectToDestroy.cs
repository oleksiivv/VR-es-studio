using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDestroy : MonoBehaviour
{
    public ParticleSystem destroyEffect, loseEffect;
    
    public ScoreController scoreController;
    
    public List<Material> materials;
    
    void Start()
    {
        GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Count)];
        
        Invoke(nameof(CleanUp), 20f);
    }
    
    void CleanUp()
    {
        Destroy(gameObject);
    }
    
    public void OnPointerEnter()
    {
        scoreController.IncrementScore();
        
        PlayEffect(destroyEffect);
        gameObject.GetComponent<SphereCollider>().enabled=false;
        gameObject.transform.localScale*=0;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.ToUpper().Equals("PLAYER"))
        {
            gameObject.GetComponent<SphereCollider>().enabled=false;
            gameObject.transform.localScale*=0;
            PlayEffect(loseEffect);
            scoreController.Restart();
        }
    }
    
    void PlayEffect(ParticleSystem effect)
    {
        effect.gameObject.transform.parent = null;
        effect.Play();
    }
}
