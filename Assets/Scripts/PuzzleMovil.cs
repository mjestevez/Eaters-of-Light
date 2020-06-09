using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMovil : MonoBehaviour
{
    public List<Button> buttons;
    public List<GameObject> secciones;
    private int index=0;
    private bool move = false;
    private float startTime;
    private float journeyLength;
    public float speed = 1f;
    private Vector3 targetPosition;
    public Vector3 offset;

    
    private void Update()
    {
        if (move)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, targetPosition, fractionOfJourney);
            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                PlayerController[] players = GetComponentsInChildren<PlayerController>();
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].inPlatform = false;
                }
                move = false;
            }
        }
        else
        {
            if (index >= buttons.Count) Destroy(this);
            if (buttons[index].active)
            {
                secciones[index].transform.parent = transform;
                targetPosition = transform.position + offset;
                startTime = Time.time;
                journeyLength = Vector3.Distance(transform.position, targetPosition);
                PlayerController[] players = GetComponentsInChildren<PlayerController>();
                for(int i=0; i < players.Length; i++)
                {
                    players[i].inPlatform = true;
                    if(!(index==0 && i==1))players[i].SetCheckPoint(offset);
                }
                MovilePlatform[] platforms = GetComponentsInChildren<MovilePlatform>();
                for (int i = 0; i < platforms.Length; i++)
                {
                    platforms[i].SetOffset(offset);
                }
                index++;
                move = true;
            }
        } 
    }

}
