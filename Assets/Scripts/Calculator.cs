using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public static Calculator instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    /// <summary>
    /// Nesne puanlarýný hesaplar
    /// </summary>
    /// <param name="swobject"></param>
    /// <returns></returns>
    public float ClaculateObjectSize(GameObject swobject)
    {
        Vector3 size = swobject.GetComponent<MeshFilter>().mesh.bounds.size;
        float objectSize = Mathf.Max(size.x, size.z);
        Debug.Log(objectSize);
        return objectSize; // Nesne boyutunu döndür
    }

    /// <summary>
    /// Yutulan nesnenin puanýný hesaplar
    /// </summary>
    /// <param name="swobject"></param>
    /// <returns></returns>
    public int CalculateObjectPoint(GameObject swobject)
    {
        Vector3 size = swobject.GetComponent<MeshFilter>().mesh.bounds.size;

        // Kenarlarý küsüratlý objelerin kenarlarýný tam sayýya yuvarla, ancak en az 1 olacak þekilde ayarla
        size = new Vector3(
            Mathf.Max(1, Mathf.RoundToInt(size.x)),
            Mathf.Max(1, Mathf.RoundToInt(size.y)),
            Mathf.Max(1, Mathf.RoundToInt(size.z))
        );

        int point = Mathf.RoundToInt(size.x * size.y * size.z);
        Debug.Log(point);
        return point; // Puaný döndür
    }

    /// <summary>
    /// Nesnenin yutulabilirliðini hesaplar
    /// </summary>
    /// <param name="holeSize"></param>
    /// <param name="swobject"></param>
    /// <returns></returns>
    public bool CanSwallow(float holeSize, GameObject swobject)
    {
        // Delik nesneden büyükse true döndür
        if (holeSize > ClaculateObjectSize(swobject))
        {
            return true;
        }
        return false;
    }
}
