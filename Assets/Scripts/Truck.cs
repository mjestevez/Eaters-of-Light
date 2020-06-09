using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    private Vector3 startPosition;
    private Transform startParent;


    private void Start()
    {
        startPosition = transform.position;
        startParent = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Limit")
        {
            transform.position = startPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = startParent;
        }
    }
}
