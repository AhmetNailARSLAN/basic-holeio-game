using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] private float holeSize; // Delik b�y�kl���
    [SerializeField] private float holeSizeFactor = 10; // B�y�me b�leni
    [SerializeField] private float moveSpeed = 5f; // Hareket h�z�


    private void Start()
    {
        // Oyun ba�lad���nda deli�in b�y�kl���n� holeSize de�i�kenine tan�mla
        Vector3 sizevector = gameObject.GetComponent<MeshFilter>().mesh.bounds.size;
        holeSize = sizevector.x;
    }
    private void Update()
    {
        // Deli�i her frame hareket ettir
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
        // �arpan nesnenin tagi objeccts ise
        if (other.tag == "objects")
        {
            // Yutulabilir mi
            if (Calculator.instance.CanSwallow(holeSize, other.gameObject))
            {
                // K�s�tlar� kald�r�r (objeyi yutar)
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }

        }
    }

    private void OnSwallow(GameObject obj)
    {
        SizeUpdate(obj);
    }

    /// <summary>
    /// Deli�i b�y�t�r
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
