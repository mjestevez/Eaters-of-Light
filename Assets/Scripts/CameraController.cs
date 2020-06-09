using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float speed=5;
    public float introSpeed = 5f;
    private bool moveCamera = false;
    private float startTime;
    private float journeyLength;
    private bool intro = true;
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(35, 0, 0), introSpeed).SetEase(Ease.InOutQuad).Play();
        transform.DOMove(target.transform.position + offset, introSpeed).SetEase(Ease.InOutQuad).OnComplete(()=> { intro = false; }).Play();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!intro)
        {
            if (moveCamera)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, fractionOfJourney);
                if (fractionOfJourney >= 1) moveCamera = false;
            }
            else
            {
                transform.position = target.transform.position + offset;
            }
        }
        
        
    }

    public void MoveCamera()
    {
        moveCamera = true;
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, target.transform.position + offset);
    }
}
