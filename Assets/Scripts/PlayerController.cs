using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Player
    {
        conejo,
        elefante
    }

    public Player tipoPlayer;
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    private float horizontalAxis = 0f;
    private float verticalAxis = 0f;
    private Rigidbody rb;
    private Animator animator;
    public float maxSpeed;
    private float speed=0;
    private bool canShoot= true;
    public float cooldown;
    private float counter = 0;
    public string shootAxisName;
    public GameObject shootGO;
    public GameObject shootPosition;
    private Vector3 checkPoint;
    public bool inPlatform = false;
    private Transform startParent;
    private Vector3 movement;
    public float rotationSpeed;
    public float dampTime = 0.1f;
    public bool disabled = false;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        checkPoint = transform.position;
        startParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            horizontalAxis = Input.GetAxis(horizontalAxisName);
            verticalAxis = Input.GetAxis(verticalAxisName);
            animator.SetFloat("Speed", speed, dampTime, Time.deltaTime);
            LookPosition();
            speed *= maxSpeed;
            if (inPlatform) transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (tipoPlayer == Player.elefante && canShoot && Input.GetAxisRaw(shootAxisName) > 0) Shoot();
            else if (!canShoot)
            {
                counter += Time.deltaTime;
                if (counter >= cooldown)
                {
                    counter = 0;
                    canShoot = true;
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!disabled)
        {
            Rotation();
            if (!inPlatform) rb.MovePosition(transform.position + (movement * speed));
        }
        
    }

    private void LookPosition()
    {
        movement = new Vector3(horizontalAxis, 0f, verticalAxis);
        speed = Mathf.Clamp(movement.magnitude, 0, 1);
        movement = movement.normalized * Time.fixedDeltaTime;

    }

    private void Rotation()
    {
        
        if(horizontalAxis!=0 || verticalAxis != 0)
        {
            Vector3 rotation = new Vector3(horizontalAxis, 0, verticalAxis);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(rotation), Time.deltaTime * rotationSpeed));
        }
    }

    public void ChangeCharacter()
    {
        speed = 0;
        counter = 0;
        horizontalAxis = 0;
        verticalAxis = 0;
        animator.SetFloat("Speed", 0f);
        this.enabled = false;
    }

    public void Shoot()
    {
        animator.SetTrigger("Shoot");
        Invoke("ShootProyectile", 0.25f);
        canShoot = false;
    }

    public void ShootProyectile()
    {
        shootGO.transform.LookAt(shootPosition.transform.right);
        GameObject pro = Instantiate(shootGO, shootPosition.transform.position, shootGO.transform.rotation);
    }

    public void ActCheckPoint()
    {
        checkPoint = transform.position;
    }

    public void HitImpact()
    {
        DisableController();
        animator.SetBool("Dead", true);
        GetComponent<AudioSource>().Play();
        Invoke("ResetPosition", 2f);
    }
    public void ResetPosition()
    {
        ActivateController();
        animator.SetBool("Dead", false);
        transform.position = checkPoint;
    }

    public void Multiplayer()
    {
        horizontalAxisName = "Horizontal2";
        verticalAxisName = "Vertical2";
        
    }

    public void SetStartParent()
    {
        transform.parent = startParent;
    }

    public void SetCheckPoint(Vector3 offset)
    {
        checkPoint += offset;
    }

    public void DisableController()
    {
        disabled = true;
        GameManager.instance.DisableControl();
    }

    public void ActivateController()
    {
        disabled = false;
        GameManager.instance.ActivateControl();
    }
}
