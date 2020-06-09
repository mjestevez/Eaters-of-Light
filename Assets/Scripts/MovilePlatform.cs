using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class Position
{
    public Vector3 position;
    public float waitTime;
}

public class MovilePlatform : MonoBehaviour
{

    public bool isActive=false;
    public List<Position> positions;
    public float speed=3f;
    public float waitTime=1f;
    private Sequence sq;

    private void Start()
    {
        sq = DOTween.Sequence();
        if (isActive) MovePlatform();
    }

    private void MovePlatform()
    {
        sq.AppendInterval(waitTime);
        for (int i=0; i < positions.Count; i++)
        {
            Vector3 p = new Vector3(positions[i].position.x, transform.position.y + positions[i].position.y, positions[i].position.z); 
            sq.Append(transform.DOMove(p, speed).SetEase(Ease.InOutQuad));
            sq.AppendInterval(positions[i].waitTime);
        }
        sq.SetLoops(999999999, LoopType.Yoyo);
        sq.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player"|| collision.gameObject.tag == "Player2")
        {
            collision.transform.parent = transform;
            collision.gameObject.GetComponent<PlayerController>().inPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<PlayerController>().SetStartParent();
            collision.gameObject.GetComponent<PlayerController>().inPlatform = false;
        }
    }

    public void Activate()
    {
        MovePlatform();
        isActive = true;
        
    }

    public void Deactivate()
    {
        sq.TogglePause();
    }
    
    public void SetOffset(Vector3 v)
    {
        for(int i=0; i < positions.Count; i++)
        {
            positions[i].position += v;
        }
    }
}
