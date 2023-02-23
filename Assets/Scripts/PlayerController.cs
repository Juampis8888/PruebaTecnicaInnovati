using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public UIController uiController;

    public float speed = 10.0f;

    public float jumpHeight = 30f; 

    public float crouchHeight = 0.5f;

    private Animator animatorPlayer;

    private bool isGrounded = true;

    private bool isRun = false;

    private bool isJump = false;

    private bool isDead = false;

    private PlayerController playerController;

    private string keyAnimation = "Run";

    void Awake()
    {
        animatorPlayer = gameObject.GetComponent<Animator>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {   
        //Correr
        if(isRun & !isDead)
        {
            float horizontalInput = 1f;
            transform.position += new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime;
            animatorPlayer.Play(keyAnimation);
        }

        // Salto
        if (isJump & isGrounded  & !isDead)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -4f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
            isJump = false;
        }

        if(isDead)
        {
            speed = 0;
            uiController.IsDead();
            animatorPlayer.Play("Idle");
            isRun = false;
            isDead = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if(collision.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
        }
    }

    public void Run()
    {
        Debug.Log("Run");
        isRun = true;
    }

    public void Stop()
    {
        Debug.Log("Stop");
        isRun = false;
        animatorPlayer.Play("Idle");
    }

    public void Jump()
    {   
        Debug.Log("Jump");
        isJump = true;
    }

    public void Bend()
    {   
        Debug.Log("Bend");
        keyAnimation = "RunBend";
    }

    public void Rise()
    {   
        Debug.Log("Rise");
        keyAnimation = "Run";
    }

    public bool GetRun()
    {
        return isRun;
    }

    public bool GetDie()
    {
        return isDead;
    }

}
