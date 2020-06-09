using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float viewAngle = 110f;
    private bool playerDetected=false;
    private Vector3 originalLookPosition;
    public float playerNoInView;
    private float counterInView=0f;
    public float coolDown = 1f;
    private float counter;
    public GameObject bullet;
    public GameObject bulletStart;
    public LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        originalLookPosition = transform.position + transform.forward;
        counter = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            counter += Time.deltaTime;
            if (counter >= coolDown)
            {
                Instantiate(bullet, bulletStart.transform.position,bulletStart.transform.rotation);
                counter = 0;
            }
            counterInView += Time.deltaTime;
            if (counterInView >= playerNoInView)
            {
                originalLookPosition.y = transform.position.y;
                transform.LookAt(originalLookPosition);
                counterInView = 0;
                playerDetected = false;
            }
        }
        else
        {
            originalLookPosition.y = transform.position.y;
            transform.LookAt(originalLookPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Proyectil")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<AudioSource>().Play();
            Invoke("Dead", 3f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            if (!pc.disabled)
            {
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle < viewAngle * 0.5f)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + Vector3.up * 0.5f, direction.normalized, out hit, Mathf.Infinity, layer))
                    {
                        Debug.DrawRay(transform.position + Vector3.up * 0.5f, direction, Color.green);
                        Debug.Log(hit.collider.gameObject.name);
                        if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Player2")
                        {
                            transform.LookAt(other.gameObject.transform);
                            playerDetected = true;
                            counterInView = 0;
                        }
                        else
                        {
                            playerDetected = false;
                        }
                    }
                }
            }
            else
            {
                playerDetected = false;
            }
            
        }
    }

    

    public void Dead()
    {
        Destroy(this.gameObject);
    }
}
