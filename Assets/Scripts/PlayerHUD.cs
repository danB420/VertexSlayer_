using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHUD : MonoBehaviour
{
    public RectTransform hpBar;

    public Stats _playerHP;
    public WeaponManager _weaponManager;
    public WaveManager waveManager;
    
    private float playerMaxHp;



    public Text WaveDisplay;
    public Text _HPtext;
    public Text _clip;
    public Text _magazine;
    public Text Timer;

    public GameObject crosshairObj;
    public GameObject reloadObj;

    private Vector2 barDimensions;
    
    private bool reloading;
    
    

    private float hpBarRatio;
    
    

    private void Start()
    {
        barDimensions = new Vector2(hpBar.sizeDelta.x, hpBar.sizeDelta.y);
        _playerHP = GameObject.Find("Player").GetComponent<Stats>();
        _weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
       
        playerMaxHp = _playerHP.hp;
       
    }

    private void Update()
    {
        hpBarRatio = (_playerHP.hp / playerMaxHp) * barDimensions.x;
        hpBar.sizeDelta = new Vector2(hpBarRatio,barDimensions.y);

        WaveDisplay.text = waveManager.currentWave.ToString();
        Timer.text = Math.Round(waveManager.timer).ToString();
        _HPtext.text = _playerHP.hp.ToString();
        _clip.text = _weaponManager.currentAmmo.ToString();;
        _magazine.text = _weaponManager.clipSize.ToString();

        reloading = _weaponManager.reloading;
        if (reloading)
        {
            crosshairObj.transform.localScale = new Vector3(0, 0, 0);
    
            reloadObj.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            crosshairObj.transform.localScale = new Vector3(1, 1, 1);
            reloadObj.transform.localScale = new Vector3(0, 0, 0);
        }


    }
}
