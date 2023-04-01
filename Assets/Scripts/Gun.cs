using System;
using System.Collections;
using System.Collections.Generic;
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

   public bool automatic;

   private int bulletsLeft, bulletsShot;

   private bool shooting, readyToShoot, reloading;

   public Camera Cam;
   public Transform shootingPos;

   //bug fixing
   public bool allowInvoke = true;

   private void Awake()
   {
      bulletsLeft = magazine;
      readyToShoot = true;
      audio = GetComponent<AudioSource>();
   }

   private void Update()
   {
      ShootingInput();
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

      Ray ray = Cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
      Vector3 targetPos;
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))
      {
         targetPos = hit.point;
      }
      else targetPos = ray.GetPoint(75);

      Vector3 dir = targetPos - shootingPos.position;
      float xSpread = Random.Range(-spread*0.5f, spread*0.5f);
      float ySpread = Random.Range(-spread*0.5f, spread*0.5f);

      Vector3 dirWithSpread = dir + new Vector3(xSpread, ySpread, 0);

      GameObject currentBullet = Instantiate(bullet, shootingPos.position, Quaternion.identity);
      currentBullet.transform.forward = dirWithSpread.normalized;
      currentBullet.GetComponent<Rigidbody>().AddForce(dirWithSpread.normalized * shootForce, ForceMode.Impulse);
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
      Debug.Log("SHOT");

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
      Debug.Log("Reloading");
      
   }

   private void ReloadFinished()
   {
      bulletsLeft = magazine;
      reloading = false;
      Debug.Log("Done");
   }
}
