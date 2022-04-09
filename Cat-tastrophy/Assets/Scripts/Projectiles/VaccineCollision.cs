using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VaccineCollision : MonoBehaviour
{
    [SerializeField] private GameObject vaccinePrefab;
    private string[] collidingTags = {"Enemy", "Building", "WaterWell", "Priest", "StandartNpc", "Villager" };
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (collidingTags.Contains(col.tag))
        {
            Destroy(vaccinePrefab);
        }
    }
}