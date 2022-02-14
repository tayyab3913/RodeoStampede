using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalControllerScript : MonoBehaviour
{
    public bool isTaken = false;
    private Rigidbody animalRb;
    private int counter = 0;

    private Collision playerCollision;
    private PlayerController playerScript;
    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        animalRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTaken == true)
        {
            LeftByPlayer();
        }
    }

    // This method checks collision and perform tasks accordingly
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isTaken)
        {
            MakeAnimalBefriended(collision);
        }
    }

    // This method makes the player ride an animal
    void MakeAnimalBefriended(Collision collision)
    {
        gameManagerScript.AddScore();
        playerCollision = collision;
        playerScript = playerCollision.gameObject.GetComponent<PlayerController>();
        collision.gameObject.GetComponent<PlayerController>().hasTakenAnimal = true;
        isTaken = true;
        animalRb.isKinematic = true;
        GetComponent<MoveBack>().enabled = false;
        transform.parent = collision.gameObject.transform;
        collision.transform.position = new Vector3(collision.transform.position.x, 2.3f, collision.transform.position.z);
        transform.position = new Vector3(collision.transform.position.x, 0.76f, collision.transform.position.z);   
        counter++;
    }

    // This method helps the player leave the animal
    void LeftByPlayer()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.parent = null;
            isTaken = false;
            GetComponent<MoveBack>().enabled = true;
        }
    }

    // This method destroys the animal based on conditions
    public void DestroyAnimal()
    {
        if(!isTaken)
        {
            Destroy(gameObject);
        } else
        {
            playerScript.Jump();
            LeftByPlayer();
            Destroy(gameObject);
        }
    }

    // This method checks for trigger and functions accordingly
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Animal"))
        {
            other.gameObject.GetComponent<AnimalControllerScript>()?.DestroyAnimal();
            DestroyAnimal();
        }
    }

    // This method sets the game manager script for this script
    public void SetGameManager(GameManager gameManagerScript)
    {
        this.gameManagerScript = gameManagerScript;
    }
}
