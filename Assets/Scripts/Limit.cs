using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            other.gameObject.GetComponent<PlayerController>().ResetPosition();
        }
    }
}
