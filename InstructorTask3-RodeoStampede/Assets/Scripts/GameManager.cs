using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public GameObject playerPrefab;
    public GameObject jumpPoint;
    public GameObject[] animalPrefabs;
    public GameObject obstaclePrefab;

    public Text scoreUI;
    public Text highScoreUI;
    public Text gameOverUI;
    public Button gameOverButtonUI;

    public int score;

    private GameObject playerInstance;

    void Start()
    {
        Time.timeScale = 1f;
        gameOverUI.gameObject.SetActive(false);
        gameOverButtonUI.gameObject.SetActive(false);
        playerInstance = Instantiate(playerPrefab, new Vector3(0, 1.5f, 0), playerPrefab.transform.rotation);
        playerInstance.GetComponent<PlayerController>().SetJumpPoint(jumpPoint);
        DisplayScore();
        SpawnAnimal();
        InvokeRepeating("SpawnAnimal", 2, 2);
        InvokeRepeating("SpawnObstacle", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
        SetHighScore();
        DisplayScore();
        GetHighScore();
    }

    // This method adds score to the total score
    public void AddScore()
    {
        score += 25;
    }

    // This method displays the total score in UI
    public void DisplayScore()
    {
        scoreUI.text = "Score: " + score;
    }

    // This method spawns an animal at a random location on the x-axis outside the screen
    public void SpawnAnimal()
    {
        int tempIndex = Random.Range(0, animalPrefabs.Length);
        GameObject animal = Instantiate(animalPrefabs[tempIndex], GetRandomSpawnPosition(), animalPrefabs[tempIndex].transform.rotation);
        animal.GetComponent<AnimalControllerScript>().SetGameManager(this);
    }

    // This method returns a random position along the x-axis outside screen
    Vector3 GetRandomSpawnPosition()
    {
        float tempRange = Random.Range(-3f, 3f);
        return new Vector3(tempRange, 1.5f, 16);
    }

    // This method spawns an obstacle
    public void SpawnObstacle()
    {
        float tempRange = Random.Range(-3f, 3f);
        GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(tempRange, 0.5f, 16), obstaclePrefab.transform.rotation);
        obstacle.GetComponent<Obstacle>().SetGameManager(this);
    }

    // This method checks to see if the game is over or not
    void GameOver()
    {
        if(gameOver)
        {
            Time.timeScale = 0f;
            gameOverUI.gameObject.SetActive(true);
            gameOverButtonUI.gameObject.SetActive(true);
        }
    }

    // This method reloads the scene
    public void ReloadGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // This method displays highscore from PlayerPrefs
    public void GetHighScore()
    {
        highScoreUI.text = "HighScore: " + PlayerPrefs.GetFloat("Highscore", 0f).ToString();
    }

    // This method sets the high score in PlayerPrefs when a high score is achieved
    public void SetHighScore()
    {
        if(score > PlayerPrefs.GetFloat("Highscore", 0f))
        {
            PlayerPrefs.SetFloat("Highscore", score);
        }
    }
}
