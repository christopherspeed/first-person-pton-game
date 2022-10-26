using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Vector3 initialPos;
    public Quaternion initialRot;
    float amountToRotate = 90f;
    bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        initialRot = transform.rotation;
        // if I set open to true initially, this needs to be open, with the rotation already performed.
    }

    public void ActivateDoor()
    {
        if (!isOpen) OpenDoor();
        else CloseDoor();
    }

    void OpenDoor()
    {
        isOpen = true;
        print("I'm open!");
    }

    void CloseDoor()
    {
        isOpen = false;
        print("I'm closed!");
    }
}
