using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotation;
    public bool isRotating;

    // Update is called once per frame
    void Update()
    {
        if (isRotating){
            transform.Rotate(rotation);
        }
        
    }

    public void ToggleRotation(){
        if (!isRotating){
            isRotating = true;
        } else {
            isRotating = false;
        }
    }
}
