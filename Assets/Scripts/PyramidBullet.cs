using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PyramidBullet : MonoBehaviour
{
   
   [Header("Audio")] 
   public AudioSource audio;

   public int enemyLayerId;

   public AudioClip hitSound;
   public List<AudioClip> gunShots;
   private int selectedClip;
   
   public Rigidbody rb;
   public BoxCollider col;
   public float projectileLifetime;
   private GameObject hit;
   public int dmg;
   private void Start()
   
   {
      selectedClip = Random.Range(0, gunShots.Count);
      audio.PlayOneShot(gunShots[selectedClip]);
      
      rb = GetComponent<Rigidbody>(); 
      col = GetComponent<BoxCollider>();
      rb.useGravity = false;
      Destroy(gameObject,projectileLifetime);
      
     
   }

   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.layer == enemyLayerId)
      {
         audio.PlayOneShot(hitSound);
         hit = collision.gameObject;
         
         hit.GetComponent<Stats>().Damage(dmg);
         Destroy(gameObject);
      }
       
   }

   
}
