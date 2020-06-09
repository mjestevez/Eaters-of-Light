using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDoor : MonoBehaviour
{
    private bool player1 = false;
    private bool player2 = false;
    public List<LightButton> lightButton;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (CheckLight() && player1 && player2) GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") player1 = true;
        if (other.gameObject.tag == "Player2") player2 = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") player1 = false;
        if (other.gameObject.tag == "Player2") player2 = false;
    }
    
    private bool CheckLight()
    {
        for(int i=0; i < lightButton.Count; i++)
        {
            if (!lightButton[i].active) return false;
        }
        return true;
    }
}
