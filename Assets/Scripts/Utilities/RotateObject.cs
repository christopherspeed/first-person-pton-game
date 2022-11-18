using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotation;
    public Vector3 randomRotation;
    public bool isRotating;
    public bool useRandomRotation;

    private void Start() {
        // generate a random rotation
        randomRotation = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating){
            if (useRandomRotation){
                transform.Rotate(randomRotation);
            } else {
                transform.Rotate(rotation);
            }
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
