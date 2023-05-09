using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    private bool showOptionMenu;
    public GameObject mainMenu;
    public GameObject optionMenu;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ShowOptionMenu()
    {
        showOptionMenu = !showOptionMenu;
        optionMenu.SetActive(showOptionMenu);
        mainMenu.SetActive(!showOptionMenu);
        
        
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
