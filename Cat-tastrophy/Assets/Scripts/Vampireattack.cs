using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampireattack : MonoBehaviour
{
    GameObject villagermanager;
    GameObject Vampiremanager;
    

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Start()
    {
        villagermanager = GameObject.Find("VillagerManager");
        Vampiremanager = GameObject.Find("EnemyManager");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("demage");

        }

        if (collision.gameObject.tag == "Villager")
        {
            Debug.Log("turning a cat into a vampire muahahahah"); // 0 = normal, 1= impf
            //Remove Villager
            collision.gameObject.SetActive(false);
            villagermanager.GetComponent<VillagerManager>().VillagerDied();
            Debug.Log(villagermanager.GetComponent<VillagerManager>().numberNormalVillagers);
            //Add Vampire, 
            //Fighting animation, 
            //stand stilll for a sec

            Vampiremanager.GetComponent<VampireManager>().numberVampires = +1;
            Vampiremanager.GetComponent<VampireManager>().Addvampire(1,collision.gameObject.GetComponent<Transform>().position);
            gameObject.GetComponent<VampireMovement>().collidedWithNpc = true;
            collision.gameObject.GetComponent<VillagerMovement>().collidedWithVampire = true;
        }

        if (collision.gameObject.tag == "immune")
        {
            Debug.Log("Oh no, my bite doesn't work");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("puhhh it'S gone!");
        }
    }
}
