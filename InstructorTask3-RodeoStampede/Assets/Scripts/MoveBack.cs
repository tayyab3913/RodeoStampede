using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    public float movementSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        MoveBackwards();
        DestroyOutsideBounds();
    }

    // This method moves the object backwards
    void MoveBackwards()
    {
        transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
    }

    // This method destroyes when the object is outside bounds
    void DestroyOutsideBounds()
    {
        if(transform.position.z < -10 && !CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    // This method is called to destroy ground
    public void GroundDestruction()
    {
        StartCoroutine(DestroyTimer());
    }

    // This method is a couroutine and destoys the ground after sometime
    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
