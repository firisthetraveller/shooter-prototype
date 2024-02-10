using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;

    private static int Count = 0;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Ammo"))
        {
            Count += 1;
            CounterText.text = "Count : " + Count;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
