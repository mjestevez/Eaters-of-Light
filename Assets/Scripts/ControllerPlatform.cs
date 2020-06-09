using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlatform : MonoBehaviour
{
    public ControllerButton up;
    public ControllerButton down;
    public ControllerButton right;
    public ControllerButton left;
    public float speed = 5f;
    private bool canMove = true;

    
    private void Update()
    {
        if (canMove)
        {
            if (up.activated) transform.Translate(Vector3.forward *speed *Time.deltaTime);
            else if (down.activated) transform.Translate(Vector3.back * speed * Time.deltaTime);
            else if (right.activated) transform.Translate(Vector3.right * speed * Time.deltaTime);
            else if (left.activated) transform.Translate(Vector3.left * speed * Time.deltaTime);

        }
        else
        {
            if(!up.activated && !down.activated && !right.activated && !left.activated)
            {
                canMove = true;
            }
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            collision.transform.parent = transform;
            collision.gameObject.GetComponent<PlayerController>().inPlatform = true;
        }
        else canMove = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<PlayerController>().SetStartParent();
            collision.gameObject.GetComponent<PlayerController>().inPlatform = false;
        }

    }
}
