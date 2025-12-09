using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInPlace : MonoBehaviour
{   
    public float rotateSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.Self);
    }
}
