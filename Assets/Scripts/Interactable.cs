using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // the action(s) you want to be executed; a more general interface that you supply with other component events
    public UnityEvent onInteract;
}
