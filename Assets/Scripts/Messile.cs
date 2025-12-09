using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messile : MonoBehaviour
{   
    public float speed = 5.0f;
    private Rigidbody messileRb;

    // Start is called before the first frame update
    void Start()
    {
        messileRb = GetComponent<Rigidbody>();
        if (messileRb == null)
        {
            Debug.LogError("Rigidbody component is missing from the missile prefab!");
        }
        else
        {
            Debug.Log("Rigidbody successfully initialized.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTowardsTarget(GameObject enemy)
    {      
        Debug.Log("MoveTowardsTarget called");
        if (enemy == null)
        {
            Debug.LogError("Enemy is null! Cannot move the missile.");
            return;
        }

        Vector3 lookDirection = (enemy.transform.position - transform.position).normalized;
        ApplyForceForDuration(5.0f, lookDirection);

        Destroy(gameObject, 5.0f);
    }

    IEnumerator ApplyForceForDuration(float duration, Vector3 direction)
    {   
        Debug.Log("ApplyForceDuration called");
        float elapsedTime = 0.0f;

        while(elapsedTime < duration)
        {
            messileRb.AddForce(direction * speed, ForceMode.Impulse);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
