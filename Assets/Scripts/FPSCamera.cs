using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
   public Transform _camTransform;
   private void Update()
   {
       transform.position = _camTransform.position;
   }
}
