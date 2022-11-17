using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotation;
    public bool isRotating;
    // Start is called before the first frame update
    void Start()
    {
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        transform.Rotate(rotation);
    }

    public void StartRotation(){
        isRotating = true;
    }
}
