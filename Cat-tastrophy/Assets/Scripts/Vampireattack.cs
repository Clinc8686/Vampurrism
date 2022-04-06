using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampireattack : MonoBehaviour
{

    

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("T R IGGGER E D");

        }

        if (collision.gameObject.tag == "Villager")
        {
            Debug.Log("turning a cat into a vampire muahahahah");
            collision.gameObject.AddComponent<VampireMovement>();
            collision.gameObject.AddComponent<Rigidbody2D>();

        }

        if (collision.gameObject.tag == "Impfgegner")
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
