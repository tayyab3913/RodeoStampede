using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 100;
    public float verticalInput;
    public float horizontalInput;
    public float jumpForce = 10;
    public float maximumSpeed = 30;
    public bool onGround = false;

    public bool hasTakenAnimal = false;
    public GameObject jumpPoint;

    

    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        jumpPoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        TakeAnimalPosition();
        PlayerJump();
        MoveJumpPoint();
    }

    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(Vector3.right * horizontalInput * movementSpeed);
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.forward * verticalInput * movementSpeed);
        SmoothMovement();
    }

    void SmoothMovement()
    {
        if ((playerRb.velocity.magnitude > maximumSpeed))
        {
            playerRb.velocity = playerRb.velocity.normalized * maximumSpeed;
        }
        if (horizontalInput == 0)
        {
            playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
            playerRb.angularVelocity = new Vector3(0, playerRb.angularVelocity.y, 0);
        }
    }

    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            Jump();
            Time.timeScale = 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && !onGround)
        {
            Time.timeScale = 1f;
            playerRb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
        }
    }

    void CheckGround(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            jumpPoint.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Animal"))
        {
            onGround = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckGround(collision);
    }

    void TakeAnimalPosition()
    {
        if(hasTakenAnimal)
        {
            transform.position = new Vector3(transform.position.x, 2.3f, transform.position.z);
        }
    }

    void MoveJumpPoint()
    {
        jumpPoint.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        onGround = false;
        jumpPoint.SetActive(true);
        hasTakenAnimal = false;
    }

    public void SetJumpPoint(GameObject jumpPoint)
    {
        this.jumpPoint = jumpPoint;
    }
}
