using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReloadSpin : MonoBehaviour
{
    private RectTransform obj;

    private void Awake()
    {
        obj = GetComponent<RectTransform>();
        StartCoroutine(rotate());
    }

    IEnumerator rotate()
    {
        obj.Rotate(0f,0f,-3f);
        yield return new WaitForSeconds(0.01f);
        yield return rotate();
    }
}
