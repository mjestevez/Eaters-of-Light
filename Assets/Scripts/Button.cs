using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public List<GameObject> plataformas;
    public List<GameObject> luces;
    public bool active = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFloor" || other.gameObject.tag == "PlayerFloor2")
        {
            GetComponent<Animator>().SetTrigger("Push");
            active = true;
            if (plataformas != null)
            {
                for(int i=0; i < plataformas.Count; i++)
                {
                    plataformas[i].GetComponent<MovilePlatform>().Activate();
                }
            }
            if (luces != null)
            {
                for (int i = 0; i < luces.Count; i++)
                {
                    luces[i].GetComponent<LightButton>().Activate();
                }
            }
        }
    }
}
