using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ExplosiveCube : MonoBehaviour
{
  public GameObject explosionPrefab;
  [Header("Audio")] 
  public AudioSource audio;

  public AudioClip explosionHiss;
  public AudioClip explosionBoom;
  public AudioClip onCollide;

  public Rigidbody rb;
  public MeshRenderer renderer;
  
  [Header("Stats")]
  public int damage;
  public float explosionRadius;
  public float explosionDelay;
  public float explosionForce;
  public int enemyLayerId;
  
  
  private void Start()
  {
    rb = GetComponent<Rigidbody>();
    audio = GetComponent<AudioSource>();
    renderer = GetComponent<MeshRenderer>();
    
    audio.PlayOneShot(explosionHiss);
    transform.rotation = Quaternion.Euler(0,0,0);
    Invoke("Explode",explosionDelay);
   
  }

  private void Explode()
  {
    
    var explosionParticles  =Instantiate(explosionPrefab, transform.position, quaternion.identity);
    explosionParticles.transform.parent = gameObject.transform;
    audio.PlayOneShot(explosionBoom);
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
    foreach (var hitCollider in hitColliders)
    {
     if(hitCollider.gameObject.layer == enemyLayerId)
     {
      hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position-transform.position  ) * explosionForce,ForceMode.Impulse);
      hitCollider.GetComponent<Stats>().Damage(damage);
     }
    }

    
    renderer.enabled = false;

    Destroy(gameObject, explosionBoom.length);
  }

  
  

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
   
    Gizmos.DrawWireSphere (transform.position ,explosionRadius);
  }
}
