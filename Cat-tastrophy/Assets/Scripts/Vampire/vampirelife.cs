using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vampirelife : MonoBehaviour
{

    public int lifepoints;
    private float counting;
    public float cooldownseconds= 1;
    bool washit;
    private int lifes=1;

    private bool instantdeath = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //find lifes here

        if (collision.gameObject.tag == "Water")
        {
            if (instantdeath == false)
            {

                if (cooldownseconds >= counting && washit == true) //do the countdown
                {
                    counting += Time.deltaTime;
                }
                if (cooldownseconds <= counting && washit == true)
                {
                    demage(1);
                    counting = 0;
                }
            }
            if (instantdeath == true)
            {
                demage(1);
            }


        }

        if (collision.gameObject.tag == "cure")
        {
            Debug.Log("Villager raised from the undead");
            

        }
    }

     private void demage(int demage)
     {
        lifes--;
        gameObject.SetActive(false);
        GameObject.Find("EnemyManager").GetComponent<VampireManager>().VampireDied();
     }
}
