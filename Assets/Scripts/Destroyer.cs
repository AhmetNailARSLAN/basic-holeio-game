using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public static Action<GameObject> OnSwallow;
    private void OnTriggerEnter(Collider other)
    {
        // Yutulma eventini ateþle
        OnSwallow?.Invoke(other.gameObject);

        // Objeyi yok et
        Destroy(other.gameObject);
    }
}
