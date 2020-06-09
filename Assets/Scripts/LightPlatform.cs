using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightPlatform : MonoBehaviour
{
    private Vector3 originalPosition;
    public float time = 5;
    private bool isFalling = false;
    public AnimationCurve easeCurve;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position; 
    }

    private void Fall()
    {
        transform.DOMoveY(-20, time).SetRelative().SetEase(easeCurve).Play();
        Invoke("FallRestart", time+0.1f);
    }
    private void FallRestart()
    {
        transform.position = originalPosition;
        isFalling = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            if (!isFalling)
            {
                isFalling = true;
                Fall();
            }
        }
    }

}
