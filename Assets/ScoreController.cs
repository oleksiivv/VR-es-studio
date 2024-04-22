using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScoreController : MonoBehaviour
{
    public TextMesh tm;
    
    private int score=0;
    
    void Start()
    {
        tm.text = "GVR VertexStudio";
        if(PlayerPrefs.GetInt("score", -1) != -1)
        {
            tm.text = "Best score: " + PlayerPrefs.GetInt("score").ToString() + "\n" + "Current: "+score.ToString();
        }
    }
    
    public void IncrementScore()
    {
        score++;
        if(score > PlayerPrefs.GetInt("score", -1))
        {
            PlayerPrefs.SetInt("score", score);
        }
        
        tm.text = "Best score: " + PlayerPrefs.GetInt("score").ToString() + "\n" + "Current: "+score.ToString();
    }
    
    public void Restart()
    {
        score=0;
        tm.text = "GAME OVER";
    }   
}
