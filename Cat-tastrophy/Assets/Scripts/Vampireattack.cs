using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampireattack : MonoBehaviour
{
    GameObject villagermanager;
    

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Start()
    {
        villagermanager = GameObject.Find("VillagerManager");
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
            //collision.gameObject.AddComponent<VampireMovement>();
            // collision.gameObject.AddComponent<Rigidbody2D>();
            collision.gameObject.SetActive(false);
            villagermanager.GetComponent<VillagerManager>().VillagerDied();
            Debug.Log(villagermanager.GetComponent<VillagerManager>().numberNormalVillagers);

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
