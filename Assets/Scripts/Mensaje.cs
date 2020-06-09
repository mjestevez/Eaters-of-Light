using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mensaje : MonoBehaviour
{
    public bool singlePlayer;
    [TextArea]
    public string textoSP;
    public bool multiPlayer;
    [TextArea]
    public string textoMP;
    private GameManager gm;
    private GameObject notificaciones;

    private void Start()
    {
        notificaciones = GameObject.FindGameObjectWithTag("Notificaciones");
        gm = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            if (singlePlayer && gm.singlePlayer)
            {
                notificaciones.GetComponentInChildren<Text>().text = textoSP;
                notificaciones.GetComponent<Animator>().SetTrigger("Activate");
                notificaciones.GetComponent<AudioSource>().Play();
            }
            if (multiPlayer && !gm.singlePlayer)
            {
                notificaciones.GetComponentInChildren<Text>().text = textoMP;
                notificaciones.GetComponent<Animator>().SetTrigger("Activate");
                notificaciones.GetComponent<AudioSource>().Play();
            }
            Destroy(this.gameObject);
        }
    }
}
