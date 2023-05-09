using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLogic : MonoBehaviour
{
    public Stats playerStats;

    public GameObject gameOverScreen;
    private bool gameOver=false;

    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
    }

    private void Update()
    {
        if (playerStats.hp < 0 || playerStats.hp == 0)
        {
            gameOverScreen.SetActive(true);
           gameOver=true;
            
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               LoadMenu();
            }
        }
        
    }

    private void LoadMenu()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        
    }
}
