using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
   public int hp;
   public int armor;
   

   public void Damage(int value)
   {
      hp -= value;
   }

   private void Update()
   {
      if (hp <= 0)
         Destroy(gameObject);
      
      
   }

   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.layer == 12)
      {
         Damage(300);
      }
   }
}
