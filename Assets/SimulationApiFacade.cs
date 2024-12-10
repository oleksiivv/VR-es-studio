using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class SimulationApiFacade : MonoBehaviour
{
    public ApiClient apiClient;

    private int _applicationId;

    private int _simulationId;

    private string _clientId;

    private string _hostId;

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

        _applicationId = applicationId;
        _hostId = hostId;

        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            www.timeout = 15;

            yield return www.Send();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.result);
            } else {
                string result=www.downloadHandler.text;

                Debug.Log(result);

                var response = JsonUtility.FromJson<JoinResponse>(result);

                _simulationId = response.id;

                Debug.Log(response);

                apiClient.Handle(response);
            }
        }
    } 

    private IEnumerator JoinSimulationAsync(InputField joinCodeField, int applicationId, string clientId){
        string url="https://vv-oasis-api-a011f855ca0a.herokuapp.com/api/join?application_id="+applicationId.ToString()+"&client_id="+clientId.ToString();

        _applicationId = applicationId;
        _clientId = clientId;

        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            www.timeout = 15;

            yield return www.Send();

            if (www.result != UnityWebRequest.Result.Success)
            {
                //
            } else {
                string result=www.downloadHandler.text;
                Debug.Log(result);

                JoinResponse response = JsonUtility.FromJson<JoinResponse>(result);

                _simulationId = response.id;

                joinCodeField.text = response.token;
                apiClient.Handle(response);
            }
        }
    }

    public void sendResults(string userId)
    {
        StartCoroutine(sendResultsAsync(userId));
    }

    public IEnumerator sendResultsAsync(string userId)
    {
        Debug.Log(userId);
        Debug.Log(_applicationId);
        Debug.Log(_simulationId);

        string url="https://vv-oasis-api-a011f855ca0a.herokuapp.com/api/metrics/death?application_id="+_applicationId.ToString()+"&user_id="+userId.ToString()+"&simulation_id="+_simulationId.ToString();

        Debug.Log(url);
        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            www.timeout = 15;

            yield return www.Send();

            if (www.result != UnityWebRequest.Result.Success) {
                //
            } else {
                //
            }
        }
    }

    public void sendNpcKillResults(string userId)
    {
        StartCoroutine(sendNpcKillResultsAsync(userId));
    }

    public IEnumerator sendNpcKillResultsAsync(string userId)
    {
        Debug.Log(userId);
        Debug.Log(_applicationId);
        Debug.Log(_simulationId);

        string url="https://vv-oasis-api-a011f855ca0a.herokuapp.com/api/metrics/npc-kill?application_id="+_applicationId.ToString()+"&user_id="+userId.ToString()+"&simulation_id="+_simulationId.ToString();

        Debug.Log(url);
        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            www.timeout = 15;

            yield return www.Send();

            if (www.result != UnityWebRequest.Result.Success) {
                //
            } else {
                //
            }
        }
    }
}
