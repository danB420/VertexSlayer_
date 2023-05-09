using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TorusProjectile : MonoBehaviour
{
    public Rigidbody rb;
    public float attractionForce;
    public float attractionRadius;
    public float lifetime;
    public GameObject spawnParticles;
    public AudioSource audio;

    public AudioClip torusShot;
    public AudioClip torusProjectile;
    public LayerMask attractable;
    public LayerMask enemy;

    private void Start()
    {
        audio.PlayOneShot(torusShot);
        audio.PlayOneShot(torusProjectile);
        rb = GetComponent<Rigidbody>();
       var particles = Instantiate(spawnParticles,transform.position,Quaternion.Euler(0,0,90));
       particles.transform.parent = gameObject.transform;
       transform.rotation = Quaternion.Euler(0,0,0);
        Destroy(gameObject,lifetime);
        Attract();
       
    }

    private void Update()
    {
       
        Invoke("Attract",0.05f);
    }

    private void Attract()
    {
        
            
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attractionForce);
       
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.gameObject.layer == 8 || hitCollider.gameObject.layer==9 )
                {
                    hitCollider.GetComponent<Rigidbody>().AddForce((hitCollider.transform.position-transform.position  ) * -attractionForce,ForceMode.Force);
                }
            }
        
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
   
        Gizmos.DrawWireSphere (transform.position ,attractionRadius);
    }
}
