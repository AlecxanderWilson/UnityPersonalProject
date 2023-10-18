using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float speed;
    private float xRange = 30.0f;
    private float yRange = 9.0f;
    public float horizontalInput;
    public float verticalInput;
    public float FireRate = 1.0f;
    public float NextFire;

    public bool hasPowerup;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Constrain();

        //if the space key is pressed then fire a projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //1 sec cooldown for firing
            if(Time.time > NextFire && !hasPowerup)
            {
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
                NextFire = Time.time + FireRate;
            }
            //powerup removes the cooldown
            else if(hasPowerup)
            {
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            }
        }
    }

    void MovePlayer()
    {
        //gets the horizontal and verticle input keys
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //moves the players based on what keys are pressed
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
    }

    //keeps the player in the bounds of the camera
    void Constrain()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }
        if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
    }

    //consumes the powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    //destroys both the player and the enemy or obstacle if they collide
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }

    //timer for the powerup
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
    }
}
