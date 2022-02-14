using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    public float movementSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackwards();
        DestroyOutsideBounds();
    }

    void MoveBackwards()
    {
        transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
    }

    void DestroyOutsideBounds()
    {
        if(transform.position.z < -10 && !CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    
    public void GroundDestruction()
    {
        StartCoroutine(DestroyTimer());
    }
    
    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
