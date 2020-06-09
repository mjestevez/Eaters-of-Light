using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime = 5f;
    private float counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        counter += Time.deltaTime;
        if (counter >= lifeTime) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colision!");
        if(collision.gameObject.tag=="Player" || collision.gameObject.tag == "Player2")
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (!pc.disabled) pc.HitImpact();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Block") Destroy(this.gameObject);
        
    }
}
