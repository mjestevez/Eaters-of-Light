using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private Rigidbody rb;
    public float impulse;
    public Vector3 angleRotation;
    public float lifeTime;
    private float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.right * -impulse, ForceMode.Impulse);
        rb.AddForce(transform.up*impulse, ForceMode.Impulse);
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= lifeTime) Destroy(this.gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion rotation = Quaternion.Euler(angleRotation * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * rotation);
    }
}
