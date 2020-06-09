using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tresure : MonoBehaviour
{
    public enum Tipo
    {
        Red,
        Blue,
        Green
    }

    private GameManager gm;
    public Tipo tipo;
    public float speed=2f;

    private void Start()
    {
        gm = GameManager.instance;
        TreasureAnimation();
    }

    private void TreasureAnimation()
    {
        transform.DOMoveY(2, speed).SetEase(Ease.InOutQuad).SetRelative().SetLoops(-1, LoopType.Yoyo).Play();
        transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), speed*1.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Play();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player" || other.gameObject.tag == "Player2")
        {
            switch (tipo)
            {
                case Tipo.Red:
                    gm.RedTreasure();
                    break;
                case Tipo.Blue:
                    gm.BlueTreasure();
                    break;
                case Tipo.Green:
                    gm.GreenTreasure();
                    break;
            }
            Destroy(gameObject);
        }
        
    }
}
