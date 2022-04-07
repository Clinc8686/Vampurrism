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


    private void OnParticleCollision(GameObject other)
    {
        //find lifes here
        Debug.LogWarning("water shot");

        if (other.gameObject.tag == "water")
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

        

        if (other.gameObject.tag == "cure")
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
