using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButton : MonoBehaviour
{
    public bool activated=false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFloor2" || other.gameObject.tag == "PlayerFloor")
        {
            GetComponent<Animator>().SetBool("Active", true);
            activated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Animator>().SetBool("Active", false);
        activated = false;
    }
}
