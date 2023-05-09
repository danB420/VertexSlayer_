using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
   private NavMeshAgent agent;
   private Transform player;
   public LayerMask whatIsGround, whatIsPlayer;
   [HideInInspector]
   public Stats playerStats;
   private bool playSound=true;
   [HideInInspector]
   public AudioSource _audio;
   [HideInInspector]
   public Animator _anim;
   [HideInInspector]
   public Rigidbody rb;
   private Vector3 walkPoint;
   private bool walkPointSet;
   public GameObject projectile;
   public AudioClip playerDetected;
   public Transform gunPoint;
   public Camera Cam;
  

   [Header("Enemy Stats")]
   public float attackCd;
   public int damage;
   public float restTime;
   public float sightRange, attackRange;
   public float walkPointRange;
   public bool canShoot;
   public float shootForce;
   
   
   
   private bool finishedPatrol = false;
   private bool resting=false;
   private bool playerInSight, playerInAttackRange, canAttack=true, canPatrol=true;
   private bool playerFound;
   private state State = state.Patrol;
   
   public enum  state
   {
      Rest=0,
      Patrol=1,
      SearchingForDestination=2,
      Chase=3,
      Attack=4
   }

   public void StateManager()
   {
      if (!playerInSight && !playerInAttackRange)
         State = state.Patrol;
      else if (playerInSight && !playerInAttackRange)  
         State = state.Chase;
      else if (playerInAttackRange && playerInSight)
         State = state.Attack;
      else State = state.Rest;
   }

   public void BehaviourManager()
   {
         if (State == state.Patrol && canPatrol)
            Patrol();
         if (State == state.Chase)
            Chase();
         if (State == state.Attack && canAttack)
            Attack();
      
   }

  
   private void Awake()
   {
      player = GameObject.Find("Player").transform;
      playerStats = player.GetComponent<Stats>();
      agent = GetComponent<NavMeshAgent>();
      _anim = GetComponentInChildren<Animator>();
      rb = GetComponentInChildren<Rigidbody>();
      _audio = GetComponent<AudioSource>();

   }

   private void Update()
   {
      StateManager();
      BehaviourManager();
      _anim.SetBool("isResting",resting);
      
   }

  
   private void FixedUpdate()
   {
      playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
      playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
   }


   private void  Patrol()
   {
      _anim.SetBool("isAttacking",false);
      resting = false;
      playSound = true;
     if (!walkPointSet) SearchWalkPoint();
     if (walkPointSet)
     {
        agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 2f)
        {
           walkPointSet = false;
           canPatrol = false;
           resting = true;
           StartCoroutine(ResetPatrol());
        }
     }
         
     
      

   }


   private void SearchWalkPoint()
   {
      float randomZ = Random.Range(-walkPointRange, walkPointRange);
      float randomX = Random.Range(-walkPointRange, walkPointRange);
      walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
      if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
      {
         walkPointSet = true;
      }
   }

   private void Chase()
   {
      _anim.SetBool("isAttacking",false);
      resting = false;
      if (playSound == true)
      {
         playSound = false;
         _audio.PlayOneShot(playerDetected);
      }
      agent.transform.LookAt(player);
      playerFound = true;
      agent.SetDestination(player.position);
     
      
   }


   private void Attack()
   {
      
      _anim.SetBool("isAttacking",true);
      resting = false;
      agent.SetDestination(transform.position);
      agent.transform.LookAt(player);
      if (canShoot)
      {
         
         Vector3 targetPos = player.transform.position;
         Vector3 dir = targetPos - gunPoint.position;
         
         GameObject currentBullet = Instantiate(projectile, gunPoint.position, Quaternion.identity);
         currentBullet.transform.forward = dir.normalized;
         currentBullet.GetComponent<Rigidbody>().AddForce(dir.normalized * shootForce, ForceMode.Impulse);
         
         
      }
      else
      playerStats.Damage(damage);
     
     canAttack = false;
     StartCoroutine(ResetAttack());
     
     
   }

   IEnumerator ResetAttack()
   {
      yield return new WaitForSeconds(attackCd);
      canAttack = true;
   }
   IEnumerator ResetPatrol()
   {
      yield return new WaitForSeconds(restTime);
      canPatrol = true;
   }
   private void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, attackRange);
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, sightRange);
   }

}
