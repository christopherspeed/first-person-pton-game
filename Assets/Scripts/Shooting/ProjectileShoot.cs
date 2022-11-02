using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileShoot : MonoBehaviour
{
    // first implementation
    [Header("Inputs and Infrastructure")]
    [SerializeField] PlayerInputManager _inputs;
    public Transform projectileOrigin; // the firing point; also provides the direction in which to fire
    private Rigidbody projectileRB;
    bool weaponEquipped = true;

    [Header("Shooting Variables")]
    public GameObject projectile;
    public float shootForce = 10f;
    public float projectileLifeTime;
    public float fireRate = 4f;
    [SerializeField] bool canFire;
    public int ammunition = 10;

    private void Awake() {
        projectileRB = projectile.GetComponent<Rigidbody>();
    }

    private void Update() {
        if (ShootPressed() && canFire){
            ShootProjectile();
            StartCoroutine(ApplyShootDelay());

        }
    }

    void ShootProjectile(){
        Vector3 dirToShoot = projectileOrigin.forward;
        GameObject shot = Instantiate(projectile, projectileOrigin);
        shot.GetComponent<Rigidbody>().velocity = dirToShoot * shootForce;
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
