using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class SimulationApiFacade : MonoBehaviour
{
    public void JoinSimulation(InputField joinCodeField, int applicationId, string clientId)
    {
        StartCoroutine(JoinSimulationAsync(joinCodeField, applicationId, clientId));
    }
    
    public void StartSimulation(int applicationId, string joinCode, string hostId)
    {
        StartCoroutine(StartSimulationAsync(applicationId, joinCode, hostId));
    }
    
    private IEnumerator StartSimulationAsync(int applicationId, string joinCode, string hostId){
        string url="https://vv-oasis-api-a011f855ca0a.herokuapp.com/api/start?application_id="+applicationId.ToString()+"&token="+joinCode.ToString()+"&host_id="+hostId.ToString();


        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            www.timeout = 15;

            yield return www.Send();

            if (www.result != UnityWebRequest.Result.Success)
            {
                //
            } else {
                string result=www.downloadHandler.text;
            }
        }
    } 

    private IEnumerator JoinSimulationAsync(InputField joinCodeField, int applicationId, string clientId){
        string url="https://vv-oasis-api-a011f855ca0a.herokuapp.com/api/join?application_id="+applicationId.ToString()+"&client_id="+clientId.ToString();


        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            www.timeout = 15;

            yield return www.Send();

            if (www.result != UnityWebRequest.Result.Success)
            {
                //
            } else {
                string result=www.downloadHandler.text;
                
                joinCodeField.text = result;
            }
        }
    } 
}
