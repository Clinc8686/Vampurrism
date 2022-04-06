using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampireattack : MonoBehaviour
{
    public float demage;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("T R IGGGER E D");
    }
}
