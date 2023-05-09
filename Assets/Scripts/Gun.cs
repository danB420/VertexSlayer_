using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


public class Gun : MonoBehaviour
{
   public GameObject bullet;
   public AudioSource audio;

   public float shootForce, upwardForce;

   public AudioClip reloadAudio;

   [Header("Gun Stats")]
   public float timeBetweenShooting, spread, reloadTime, fireRate;
   public int magazine, bulletsPerShot;
   public int dmg;

   
   public bool automatic;

   private int bulletsLeft, bulletsShot;

   private bool shooting, readyToShoot, reloading;
  private  Vector3 targetPos=new Vector3(0,0,0);

   public Camera Cam;
   public Transform shootingPos;
   public WeaponManager _wepManager;

   //bug fixing
   public bool allowInvoke = true;

   private void Awake()
   {
      bulletsLeft = magazine;
      readyToShoot = true;
      audio = GetComponent<AudioSource>();
      _wepManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
   }

   private void Update()
   {
     
      ShootingInput();
      _wepManager.currentAmmo = bulletsLeft;
      _wepManager.clipSize = magazine;
      _wepManager.reloading = reloading;

   }

   private void ShootingInput()
   {

      if (automatic) shooting = Input.GetKey(KeyCode.Mouse0);
      else shooting = Input.GetKeyDown(KeyCode.Mouse0);
      if (readyToShoot && bulletsLeft > 0 && !reloading && shooting)
      {
         bulletsShot = 0;
         Shoot();
      }
      if(readyToShoot  &&  !reloading &&bulletsLeft ==0)
         Reload();

      
      if (Input.GetKeyDown(KeyCode.R) && bulletsLeft !=magazine && !reloading) 
      {
         Reload();
      }
   }

   private void Shoot()
   {
      readyToShoot = false;
      
      
      RaycastHit hit;
      if (Physics.Raycast(shootingPos.position,shootingPos.forward,out hit,200f))
      {
         targetPos = hit.point;
      }
      Vector3 dir = targetPos - shootingPos.position;

         float xSpread = Random.Range(-spread*0.5f, spread*0.5f);
      float ySpread = Random.Range(-spread*0.5f, spread*0.5f);

      

      GameObject currentBullet = Instantiate(bullet, shootingPos.position, Quaternion.identity);
      currentBullet.transform.forward = dir;
      currentBullet.GetComponent<Rigidbody>().AddForce(dir.normalized * shootForce, ForceMode.Impulse);
      currentBullet.GetComponent<Rigidbody>().AddForce(Cam.transform.up * upwardForce, ForceMode.Impulse);

      bulletsLeft--;
      bulletsShot++;

      if (allowInvoke)
      {
         Invoke("ResetShot", timeBetweenShooting);
         allowInvoke = false;
      }

      if (bulletsShot < bulletsPerShot && bulletsLeft > 0)
         Invoke("Shoot", fireRate);
      

   }

   private void ResetShot()
   {
      readyToShoot = true;
      allowInvoke = true;
   }

   private void Reload()
   {
      audio.PlayOneShot(reloadAudio);
      reloading = true;
      Invoke("ReloadFinished",reloadTime);
      
      
   }

   private void ReloadFinished()
   {
      bulletsLeft = magazine;
      reloading = false;
     
   }
}
