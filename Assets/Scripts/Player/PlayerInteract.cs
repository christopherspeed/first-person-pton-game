using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask InteractibleLayer;
    UnityEvent interactAction;
    public PlayerInputManager _inputs;
    bool canInteract = true;
    float interactionDelay = .2f;
    
    private void Update() {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 2, InteractibleLayer)){
            if (hitInfo.collider.GetComponent<Interactable>()){
                interactAction = hitInfo.collider.GetComponent<Interactable>().onInteract;
                if (InteractPressed()){
                    interactAction.Invoke();
                }
            }
        }
    }

    
    bool InteractPressed(){
        if (canInteract) 
        {
            StartCoroutine(InteractionDelay()); // implement a slight delay
            return _inputs.InteractionInput;
        }
        else return false;
    }
    
    IEnumerator InteractionDelay (){
        canInteract = false;
        yield return new WaitForSeconds(interactionDelay);
        canInteract = true;
    }
}
