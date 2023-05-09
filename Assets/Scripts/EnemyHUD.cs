using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public RectTransform hpBar;

    public Stats enemyHP;
    private float enemyMaxHp;
    
    private Vector2 barDimensions;
    private float hpBarRatio;
    
    

    private void Start()
    {
        barDimensions = new Vector2(hpBar.sizeDelta.x, hpBar.sizeDelta.y);
        enemyHP = GetComponentInParent<Stats>();
       
        enemyMaxHp = enemyHP.hp;
       
    }

    private void Update()
    {
        hpBarRatio = (enemyHP.hp / enemyMaxHp) * barDimensions.x;
        hpBar.sizeDelta = new Vector2(hpBarRatio,barDimensions.y);

    }

}
