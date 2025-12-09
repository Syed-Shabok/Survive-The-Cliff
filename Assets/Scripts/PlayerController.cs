using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float speed = 5.0f;
    private float powerupStrength = 30.0f;    
    public bool hasPowerup = false;
    public bool hasPowerup2 = false;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject messilePrefab;
    public GameObject powerupIndicator;
    private SpawnManager spawnManager;

    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {   
        powerupIndicator.transform.position = transform.position;
        //powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        //LaunchMessile();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {   
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
        else if(other.CompareTag("Powerup2"))
        {
            hasPowerup2 = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine2());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator PowerupCountdownRoutine2()
    {
        yield return new WaitForSeconds(7);
        hasPowerup2 = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    // private void LaunchMessile()
    // {   

    //     if (Input.GetKeyDown(KeyCode.F) && hasPowerup2)
    //     {   
    //         Debug.Log("LauchMessile Attemped");
    //         GameObject[] currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");

    //         foreach (GameObject enemy in currentEnemies)
    //         {
    //             Vector3 offset = new Vector3(1.0f, 0, 0);
    //             GameObject currentMissile = Instantiate(messilePrefab, transform.position + offset, messilePrefab.transform.rotation);

    //             // Validate missile script
    //             Messile missileScript = currentMissile.GetComponent<Messile>();
    //             if (missileScript == null)
    //             {
    //                 Debug.LogError("Missile prefab does not have the Messile script attached!");
    //                 return;
    //             }
    //             Rigidbody messileRb = currentMissile.GetComponent<Rigidbody>();

    //             // Move missile towards the enemy
    //             missileScript.MoveTowardsTarget(enemy);
                
    //             // Vector3 lookDirection = (enemy.transform.position - transform.position).normalized;
    //             // messileRb.AddForce(lookDirection * speed, ForceMode.Impulse);

    //             //Destroy(gameObject, 5.0f);
    //         }
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {   
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Has collided with " + collision.gameObject.name + " with powerup set to: " + hasPowerup);
        }
    }
    
}
