using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using UnityEngine.UI;

public class UserDisplay : MonoBehaviour
{
    private ApiClient apiClient;

    public Text display;

    public SkinnedMeshRenderer mesh;

    public Material otherTeamMaterial;

    void Start()
    {
        apiClient = GameObject.Find("ApiClient").GetComponent<ApiClient>();

        display.text = apiClient.currentPlayerData.team.ToString()+"\n"
            + "Scenario: "+apiClient.currentPlayerData.scenario.ToString()+"\n"
            + "Target: "+apiClient.currentPlayerData.type;

        Invoke(nameof(Refresh), 3f);
    }

    void Refresh()
    {
        display.text = apiClient.currentPlayerData.team.ToString()+"\n"
            + "Scenario: "+apiClient.currentPlayerData.scenario.ToString()+"\n"
            + "Target: "+apiClient.currentPlayerData.type;

        Invoke(nameof(Refresh), 10f);

        Debug.Log("Change client look");

        if(NetworkManager.Singleton.LocalClientId % 2 == 0)
        {
            Debug.Log("Change client look material");

            Material[] materials = mesh.materials;
            materials[0] = otherTeamMaterial;
            materials[1] = otherTeamMaterial;
            materials[2] = otherTeamMaterial;
            mesh.materials = materials;
        }
    }
}
