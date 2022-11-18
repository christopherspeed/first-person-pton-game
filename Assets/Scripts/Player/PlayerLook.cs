using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerLook : MonoBehaviour
{
    [Header("Look")]
    public float horizontalSensitivity = 10f;
    public float verticalSensitivity = 5f;

    [SerializeField] Transform cam;
    [SerializeField] Transform playerOrientation;

    private float mouseX;
    private float mouseY;

    private float xRotation;
    private float yRotation;

    private float multiplier = 0.01f;

    [Header("Input")]
    [SerializeField] PlayerInputManager _inputs;

    private void Start()
    {
        // hide our cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GetInput();
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerOrientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void GetInput()
    {
        GetVerticalInput();
        GetHorizontalInput();

        yRotation += mouseX * horizontalSensitivity * multiplier; // we rotate around the y axis to look horizontally
        xRotation -= mouseY * verticalSensitivity * multiplier;

        // clamp, to prevent flipping
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }


    #region Update Look Variables from Outside Class
    private void GetVerticalInput()
    {
        mouseY = _inputs.LookInput.y;
    }

    private void GetHorizontalInput()
    {
        mouseX = _inputs.LookInput.x;
    }
    #endregion
}
