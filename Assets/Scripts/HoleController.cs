using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] private float holeSize; // Delik büyüklüðü
    [SerializeField] private float holeSizeFactor = 10; // Büyüme böleni
    [SerializeField] private float moveSpeed = 5f; // Hareket hýzý


    private void Start()
    {
        // Oyun baþladýðýnda deliðin büyüklüðünü holeSize deðiþkenine tanýmla
        Vector3 sizevector = gameObject.GetComponent<MeshFilter>().mesh.bounds.size;
        holeSize = sizevector.x;
    }
    private void Update()
    {
        // Deliði her frame hareket ettir
        HoleMovement();
    }

    void HoleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Çarpan nesnenin tagi objeccts ise
        if (other.tag == "objects")
        {
            // Yutulabilir mi
            if (Calculator.instance.CanSwallow(holeSize, other.gameObject))
            {
                // Kýsýtlarý kaldýrýr (objeyi yutar)
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }

        }
    }

    private void OnSwallow(GameObject obj)
    {
        SizeUpdate(obj);
    }

    /// <summary>
    /// Deliði büyütür
    /// </summary>
    /// <param name="swobject"></param>
    public void SizeUpdate(GameObject swobject)
    {
        float objectSize =  Calculator.instance.ClaculateObjectSize(swobject) / holeSizeFactor;
        holeSize += objectSize;
        gameObject.transform.localScale += new Vector3(objectSize, 0, objectSize);

    }
    private void OnEnable()
    {
        Destroyer.OnSwallow += OnSwallow;
    }

    private void OnDisable()
    {
        Destroyer.OnSwallow -= OnSwallow;
    }
}
