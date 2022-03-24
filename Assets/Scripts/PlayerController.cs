using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpSpeed;
    public float walkSpeed;
    public AudioSource coinSound;
    public AudioSource jumpSound;
    public AudioSource deathSound;
    
    private Rigidbody rb;
    private Collider col;
    private bool jumping = false;

    private Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        size = col.bounds.size;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WalkHandler();
        JumpHandler();
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    private void WalkHandler()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis * Time.deltaTime * walkSpeed, 0, vAxis * Time.deltaTime * walkSpeed);

        Vector3 newPos = transform.position + movement;

        if (transform.position.y < -3)
            GameManager.instance.LoseTry();

        rb.MovePosition(newPos);

        if (hAxis != 0 || vAxis != 0)
        {
            Vector3 direction = new Vector3(hAxis, 0, vAxis);

            rb.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void JumpHandler()
    {
        float jAxis = Input.GetAxis("Jump");

        if (jAxis > 0)
        {
            if (!jumping && IsGrounded())
            {
                jumpSound.Play();
                jumping = true;

                Vector3 jumpVector = new Vector3(0, jAxis * jumpSpeed, 0);

                rb.AddForce(jumpVector, ForceMode.VelocityChange);
            }
        }
        else
            jumping = false;
    }

    private bool IsGrounded()
    {
        Vector3 corner1 = transform.position + new Vector3(size.x / 2,  -size.y / 2 + 0.1f,  size.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.1f,  size.z / 2);
        Vector3 corner3 = transform.position + new Vector3(size.x / 2,  -size.y / 2 + 0.1f, -size.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.1f, -size.z / 2);

        bool grounded1 = Physics.Raycast(corner1, Vector3.down, .2f);
        bool grounded2 = Physics.Raycast(corner2, Vector3.down, .2f);
        bool grounded3 = Physics.Raycast(corner3, Vector3.down, .2f);
        bool grounded4 = Physics.Raycast(corner4, Vector3.down, .2f);

        return grounded1 || grounded2 || grounded3 || grounded4;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Coin":
                coinSound.Play();
                GameManager.instance.IncreaseScore(1);
                Destroy(other.gameObject);
                break;
            case "Enemy":
                deathSound.Play();
                GameManager.instance.LoseTry();
                break;
            case "Goal":
                GameManager.instance.IncreaseLevel();
                break;
        }
    }
}
