using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> guns;

    public Camera camera;
    
    private int selectedWeapon;
    public void Start()
    {
        
        SwapWeapons();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
              SwapWeapons();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
            SwapWeapons();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
            SwapWeapons();
        }
        
          
        
    }

    private void SwapWeapons()
    {
        Debug.Log("Gun selected :" + guns[selectedWeapon]);
        for (int i = 0; i < guns.Count; i++)
        {
            if (i == selectedWeapon )
            {
                guns[i].SetActive(true);
                camera.GetComponent<MouseMovement>().gun = guns[selectedWeapon].transform;
                if (i == 2)
                {
                    guns[i].GetComponent<TorusGun>().Cam = camera;
                }
                else guns[i].GetComponent<Gun>().Cam = camera;

            }
                
            else
                guns[i].SetActive(false);

        }
    }
    
   
}
