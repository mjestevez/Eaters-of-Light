using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyButton : MonoBehaviour
{
    public List<GameObject> activables;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFloor2")
        {
            GetComponent<Animator>().SetBool("Active", true);
            if (activables != null)
            {
                for (int i = 0; i < activables.Count; i++)
                {
                    activables[i].GetComponent<MovilePlatform>().Activate();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Animator>().SetBool("Active", false);
        if (activables != null)
        {
            for (int i = 0; i < activables.Count; i++)
            {
                activables[i].GetComponent<MovilePlatform>().Deactivate();
            }
        }
    }
}
