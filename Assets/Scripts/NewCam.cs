using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCam : MonoBehaviour
{
    public GameObject cam;
    private bool p1 = false;
    private bool p2 = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player2")
        {
            p2 = true;
            cam.SetActive(true);
        }else if(other.gameObject.tag == "Player")
        {
            p1 = true;
            cam.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player2")
        {
            p2 = false;
            if(!p1)cam.SetActive(false);
        }
        else if (other.gameObject.tag == "Player")
        {
            p1 = false;
            if (!p2) cam.SetActive(false);
        }
    }
}
