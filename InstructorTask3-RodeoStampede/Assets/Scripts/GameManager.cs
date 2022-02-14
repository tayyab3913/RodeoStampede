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

    public void AddScore()
    {
        score += 25;
    }

    public void DisplayScore()
    {
        scoreUI.text = "Score: " + score;
    }

    public void SpawnAnimal()
    {
        int tempIndex = Random.Range(0, animalPrefabs.Length);
        GameObject animal = Instantiate(animalPrefabs[tempIndex], GetRandomSpawnPosition(), animalPrefabs[tempIndex].transform.rotation);
        animal.GetComponent<AnimalControllerScript>().SetGameManager(this);
    }

    Vector3 GetRandomSpawnPosition()
    {
        float tempRange = Random.Range(-3f, 3f);
        return new Vector3(tempRange, 1.5f, 16);
    }

    public void SpawnObstacle()
    {
        float tempRange = Random.Range(-3f, 3f);
        GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(tempRange, 0.5f, 16), obstaclePrefab.transform.rotation);
        obstacle.GetComponent<Obstacle>().SetGameManager(this);
    }

    void GameOver()
    {
        if(gameOver)
        {
            Time.timeScale = 0f;
            gameOverUI.gameObject.SetActive(true);
            gameOverButtonUI.gameObject.SetActive(true);
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GetHighScore()
    {
        highScoreUI.text = "HighScore: " + PlayerPrefs.GetFloat("Highscore", 0f).ToString();
    }

    public void SetHighScore()
    {
        if(score > PlayerPrefs.GetFloat("Highscore", 0f))
        {
            PlayerPrefs.SetFloat("Highscore", score);
        }
    }
}
