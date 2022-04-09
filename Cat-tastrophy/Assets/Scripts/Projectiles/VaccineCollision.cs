using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineCollision : MonoBehaviour
{
    [SerializeField] private GameObject vaccinePrefab;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Alarm: " + col.name + "  " + col.tag);
        if (col.CompareTag("Enemy"))
        {
            Destroy(vaccinePrefab);
        }
    }
}
