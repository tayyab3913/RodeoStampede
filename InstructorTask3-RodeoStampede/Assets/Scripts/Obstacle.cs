using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameManager gameManager;

    // This method checks for trigger and destorys game objects according to different logics
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Animal"))
        {
            Destroy(gameObject);
            AnimalControllerScript tempAnimalScript = other.GetComponent<AnimalControllerScript>();
            if(tempAnimalScript != null)
            {
                tempAnimalScript.DestroyAnimal();
            }
        }
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameManager.gameOver = true;
        }
    }

    // This method sets the game manager reference to this script
    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
