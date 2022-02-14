using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneration : MonoBehaviour
{
    public GameObject[] groundPrefabs;
    public GameObject spawnPoint;
    private GameObject ground;
    private float leftGroundBound = 0;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGround();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateProceduralGround();
    }

    // This method generates a piece of ground when the game starts
    void GenerateGround()
    {
        int tempIndex = Random.Range(0, groundPrefabs.Length);
        ground = Instantiate(groundPrefabs[tempIndex], spawnPoint.transform.position, groundPrefabs[tempIndex].transform.rotation);
    }

    // This method generates more ground and destroys the one that is outside screen
    void GenerateProceduralGround()
    {
        if (ground.transform.position.z < leftGroundBound)
        {
            ground.GetComponent<MoveBack>().GroundDestruction();
            int tempIndex = Random.Range(0, groundPrefabs.Length);
            ground = Instantiate(groundPrefabs[tempIndex], spawnPoint.transform.position, groundPrefabs[tempIndex].transform.rotation);
        }
    }
}
