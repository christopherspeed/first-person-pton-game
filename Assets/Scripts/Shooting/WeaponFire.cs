using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    // first implementation
    [Header("Inputs and Infrastructure")]
    [SerializeField] PlayerInputManager _inputs;
    bool weaponEquipped = true;

    [Header("Shooting Variables")]
    public float fireRate = 4f;
    [SerializeField] float bulletRange = 20f;
    [SerializeField] bool canFire;
    public int ammunition = 10;

    private void Update() {
        if (ShootPressed() && canFire){
            Shoot();
            StartCoroutine(ApplyShootDelay());

        }
    }

    void Shoot(){
        
    }

    // applies a shooting delay; if the 
    private IEnumerator ApplyShootDelay(){
        canFire = false;
        yield return new WaitForSeconds(1 / fireRate);
        canFire = true;
    }

    private bool ShootPressed(){
        return _inputs.ShootInput;
    }
}
