using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private PlayerController playerControllerScript;

    private float xBound = 38.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //destroys the object if it exceeds the upper or lower x bound
        if (transform.position.x > xBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -xBound)
        {
            Destroy(gameObject);
        }

        //destroys all objects if the player dies
        if (playerControllerScript.gameOver == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
