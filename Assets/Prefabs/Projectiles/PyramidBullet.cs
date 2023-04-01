using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PyramidBullet : MonoBehaviour
{
   
   [Header("Audio")] 
   public AudioSource audio;


   public AudioClip hitSound;
   public List<AudioClip> gunShots;
   private int selectedClip;
   
   public Rigidbody rb;
   public MeshCollider col;
   public float projectileLifetime;

   private void Start()


   {
      selectedClip = Random.Range(0, gunShots.Count);
      audio.PlayOneShot(gunShots[selectedClip]);
      
      rb = GetComponent<Rigidbody>(); 
      col = GetComponent<MeshCollider>();
      rb.useGravity = false;
      Destroy(gameObject,projectileLifetime);
     
   }

   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.layer == 8)
      {
         audio.PlayOneShot(hitSound);
         Debug.Log(collision.gameObject.name);
         if(!audio.isPlaying)
            Destroy(gameObject);
      }
       else if(!audio.isPlaying) Destroy(gameObject);
   }

   
}
