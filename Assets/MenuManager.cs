using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    UXAction action;

    private void Awake() {
        action = new UXAction();

        action.State.QuitGame.performed += _ => QuitGame();
    }


    public void QuitGame()
    {
        Debug.Log("Quit Game"); // for testing in editor
        Application.Quit();
    }

    private void OnEnable() {
        action.Enable();
    }

    private void OnDisable() {
        action.Disable();
    }
}
