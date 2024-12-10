using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JoinResponse
{
    public int id = -1;

    public string team = "";

    public string token;

    public string type = "";

    public string location = "";

    public string members = "1";

    public string scenario = "";

    public bool geo = false;

    public string map = "";
}
