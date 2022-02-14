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

    void GenerateGround()
    {
        int tempIndex = Random.Range(0, groundPrefabs.Length);
        ground = Instantiate(groundPrefabs[tempIndex], spawnPoint.transform.position, groundPrefabs[tempIndex].transform.rotation);
    }

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
