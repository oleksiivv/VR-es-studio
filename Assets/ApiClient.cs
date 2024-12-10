using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using Unity.Netcode;
using System;

public class ApiClient : NetworkBehaviour
{
    public GameObject forestLocation, cityLocation, buildingLocation;

    public JoinResponse currentPlayerData;

    public NPCController npcs;

    public int size = 256;//for forest map

    public void Handle(JoinResponse response)
    {
        currentPlayerData = response;

        Debug.Log(response.map);
        switch(response.location)
        {
            case "forest":
                loadForest();
                break;
            case "buidling":
                loadBuilding();
                break;
            case "city":
                loadCity();
                break;
        }

        npcs.Spawn();
    }

    public void loadCity()
    {
        cityLocation.SetActive(true);

        forestLocation.SetActive(false);
        buildingLocation.SetActive(false);
    }

    public void loadBuilding()
    {
        buildingLocation.SetActive(true);

        cityLocation.SetActive(false);
        forestLocation.SetActive(false);
    }

    public void loadForest()
    {
        try
        {
            if (GenerateMeshFromImage(ConvertStringToTexture2D(currentPlayerData.map)))
            {
                Debug.Log("Mesh terrain successfully generated.");
            }
            else
            {
                Debug.LogError("Failed to generate mesh terrain.");
                loadDefaultForest();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Exception occurred: " + ex.Message);
            loadDefaultForest();
        }
    }

    bool GenerateMeshFromImage(Texture2D map)
    {
        if (map == null)
            return false;

        GenerateMesh(map);
        return true;
    }

    private void loadDefaultForest()
    {
        forestLocation.SetActive(true);

        cityLocation.SetActive(false);
        buildingLocation.SetActive(false);
    }

    void GenerateMesh(Texture2D heightMap)
    {
        Vector3[] vertices = new Vector3[(size + 1) * (size + 1)];
        int[] triangles = new int[size * size * 6];
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0, y = 0; y <= size; y++)
        {
            for (int x = 0; x <= size; x++, i++)
            {
                float height = heightMap.GetPixel(x * heightMap.width / size, y * heightMap.height / size).grayscale * 2;
                vertices[i] = new Vector3(x, height, y);
                uv[i] = new Vector2((float)x / size, (float)y / size);
            }
        }

        int vert = 0;
        int tris = 0;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size + 1;
                triangles[tris + 5] = vert + size + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private Texture2D ConvertStringToTexture2D(string encodedString)
    {
        byte[] imageData = Convert.FromBase64String(encodedString);

        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(imageData)) // LoadImage automatically resizes the texture dimensions.
        {
            return texture;
        }
        else
        {
            Debug.LogError("Could not decode image from provided string.");
            return null;
        }
    }
}
