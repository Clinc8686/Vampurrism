using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vampirelife : MonoBehaviour
{

    public int lifepoints;
    private float counting;
    public float cooldownseconds= 1;
    bool washit;
    private int lifes=1;
    bool alreadydying = false;

    private int secondstowait =5;
    private float catpause = 0;

    private Transform thisvampire;
    private float scaleme=1;


    private void Start()
    {
        thisvampire = GetComponent<Transform>();
    }

    private void OnParticleCollision(GameObject other)
    {
        //find lifes here
        Debug.LogWarning("water shot");

        if (other.gameObject.tag == "water")
        {
            demage(1);

        }

        if (other.gameObject.tag == "cure")
        {
            Debug.Log("Villager raised from the undead");
            

        }  
    }

    private void Update()
    {
        if (lifes <= 0)
        {
            fadeToBlack();
        }

    }

    private void demage(int demage)
    {
        lifes--;
        if (lifes <= 0&& alreadydying==false)
        {
            this.gameObject.tag = "Untagged";
            alreadydying = true;
            GameObject.Find("EnemyManager").GetComponent<VampireManager>().VampireDied();
            this.GetComponent<Animator>().SetBool("dying", true);
           // gameObject.GetComponent<VampireMovement>().collidedWithNpc = true;
            gameObject.GetComponent<VampireMovement>().enabled = false;

        }

    }

    private void fadeToBlack()
    {
        if (secondstowait <= catpause) //prepare the countdown, something collided.
        {
            catpause = 0;
            scaleme = 1;
        }

        if (secondstowait >= catpause) //do the countdown
        {
            if(catpause>1 && scaleme > 0)
            {
               scaleme= scaleme - 0.4f * Time.deltaTime;
            }
            catpause += Time.deltaTime;

            thisvampire.localScale = new Vector3(scaleme, scaleme, scaleme);
        }

        if (secondstowait <= catpause)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }
}
