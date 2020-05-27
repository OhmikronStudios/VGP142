using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    CharacterController cc;
    Animator anim;
    //Rigidbody rb;

    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravitySpeed;
    public float projectileSpeed = 10.0f;

    [SerializeField] GameObject arrow;
    [SerializeField] GameObject arrowSpawn;

    // Setting type to 0 means use SimpleMove()
    // Setting type to 1 means use Move()
    [SerializeField] int type = 1;

    Vector3 moveDirection;

    //[SerializeField] bool isGodMode;
    //[SerializeField] float timerGodMode;
    
    //[SerializeField] float jumpBoost;
    //[SerializeField] float timerJumpBoost;
   



    // Start is called before the first frame update
    void Start()
    {
        try
        {
            cc = GetComponent<CharacterController>();
            //rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();

            if (speed <= 0)
            { speed = 6.0f; Debug.Log("Speed not set on" + name + "defaulting to" + speed); }

            if (jumpSpeed <= 0)
            { jumpSpeed = 8.0f; Debug.Log("Speed not set on" + name + "defaulting to" + jumpSpeed); }

            if (rotationSpeed <= 0)
            { rotationSpeed = 10.0f; Debug.Log("Speed not set on" + name + "defaulting to" + rotationSpeed); }

            if (gravitySpeed <= 0)
            { gravitySpeed = 9.81f; Debug.Log("Speed not set on" + name + "defaulting to" + gravitySpeed); }

            //isGodMode = false;
            //if (timerGodMode <= 0)
            //{ timerGodMode = 2.0f; Debug.Log("timerGodMode not set on" + name + "defaulting to" + timerGodMode); }
            //if (jumpBoost <= 0)
            //{ timerGodMode = 20.0f; Debug.Log("JumpBoost not set on" + name + "defaulting to" + jumpBoost); }
            //if (timerJumpBoost <= 0)
            //{ timerGodMode = 5.0f; Debug.Log("timerJumpBoost not set on" + name + "defaulting to" + timerJumpBoost); }

            moveDirection = Vector3.zero;
        }

        catch (ArgumentNullException e)
        { Debug.LogWarning(e.Message); }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();

        RaycastHit hit;
        Debug.DrawRay(arrowSpawn.transform.position, arrowSpawn.transform.forward * 15.0f, Color.red);
        if (Physics.Raycast(arrowSpawn.transform.position, arrowSpawn.transform.forward, out hit, 15.0f))
        {
            //Debug.Log("Raycast hit " + hit.transform.name);
            //if (hit.transform.tag == "Enemy")
            //{
            //    hit.transform.GetComponent<Sheep>().Freeze();
            //}
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.R))
        { Punch(); }
        else if (Input.GetKeyDown(KeyCode.E))
        { Kick(); }
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("FireArrow");
            FireArrow();
        }

    }

    private void Movement()
    {
        // Using SimpleMove()
        if (type == 0)
        {
            //use if not using MouseLook.CS
            //transform.Rotate (0, Input.GetAxis("Horizontal" * rotationSpeed, 0);

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = Input.GetAxis("Vertical") * speed;
            cc.SimpleMove(forward * curSpeed);
          
        }

        else if (type == 1)
        {
            if (cc.isGrounded)
            {

                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

                //use if not using MouseLook.CS
                //transform.Rotate (0, Input.GetAxis("Horizontal" * rotationSpeed, 0);

                moveDirection = transform.TransformDirection(moveDirection);
                
                moveDirection *= speed;
                anim.SetFloat("Speed", moveDirection.z);
                if (Input.GetButtonDown("Jump"))
                    moveDirection.y = jumpSpeed;
            }

            moveDirection.y -= gravitySpeed * Time.deltaTime;

            cc.Move(moveDirection * Time.deltaTime);


        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("CharacterController colliding with " + hit.gameObject.name);
        if (hit.gameObject.tag == "Projectile")
        {
            SceneManager.LoadScene(0);
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("OnTriggerEnter: " + other.gameObject.name);
    //    if (other.CompareTag("PowerUp_Godmode"))
    //    {
    //        isGodMode = true;
    //        Destroy(other.gameObject);
    //        StartCoroutine(stopGodMode());
    //    }
    //    if (other.CompareTag("PowerUp_Superjump"))
    //    {
    //        jumpSpeed += jumpBoost;
    //        Destroy(other.gameObject);
    //        StartCoroutine(stopJumpBoost());
    //    }
    //}

    private void Punch()
    {
        anim.SetTrigger("Attack");
    }
    private void Kick()
    {
        anim.SetTrigger("Attack2");
    }
    private void FireArrow()
    {
        Rigidbody temp = Instantiate(arrow.GetComponent<Rigidbody>(), arrowSpawn.transform.position, arrowSpawn.transform.rotation);
        temp.AddForce(arrowSpawn.transform.forward * projectileSpeed, ForceMode.Impulse);
    }

    //IEnumerator stopGodMode()
    //{
    //    yield return new WaitForSeconds(timerGodMode);
    //    isGodMode = false;
    //}
    //IEnumerator stopJumpBoost()
    //{
    //    yield return new WaitForSeconds(timerJumpBoost);
    //    jumpSpeed -= jumpBoost;
    //}




}
