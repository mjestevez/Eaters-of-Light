using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    public bool active = false;
    public Material newMaterial;

    public void Activate()
    {
        active = true;
        GetComponent<MeshRenderer>().material = newMaterial;
    }
}
