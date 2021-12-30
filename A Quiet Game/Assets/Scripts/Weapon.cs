using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weapon : MonoBehaviour
{    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    public AudioClip activateSound;
    public AudioClip shootSound;
    public AudioClip pickupSound;

    //bools 
    bool shooting, readyToShoot, reloading;

    private bool active;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public string getName(){
        return gameObject.name;
    }
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        if (active) {
            MyInput();
        }
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetButton("Shoot");
        else shooting = Input.GetButtonDown("Shoot");
        
        if(bulletsLeft == 0){
           Invoke("Reload", timeBetweenShots);
        } 
        
        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        Debug.Log("Shoot");
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
        }

        bulletsLeft--;
        bulletsShot--;
        Debug.Log("Bullets Left: " + bulletsLeft + "bulletsShot: " + bulletsShot);

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

         
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void OnTriggerEnter(Collider other) {
         if(other.CompareTag("Player") ){
             other.GetComponent<Player>().PickupWeapon(this);
         }
    }

    public void ActivateWeapon() {
        active = true;
        Debug.Log("Weapon Activated");
    }
}


