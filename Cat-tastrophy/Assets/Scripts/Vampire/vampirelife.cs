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

    private int secondstowait =5;
    private float catpause = 0;
    float colora=255;

    private SpriteRenderer colorkeeper;

    private void Start()
    {
        colorkeeper = GetComponent<SpriteRenderer>();
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
        if (lifes <= 0)
        {
            GameObject.Find("EnemyManager").GetComponent<VampireManager>().VampireDied();
            this.GetComponent<Animator>().SetBool("dying", true);
            gameObject.GetComponent<VampireMovement>().collidedWithNpc = true;
        }

    }

    private void fadeToBlack()
    {
        if (secondstowait <= catpause) //prepare the countdown, something collided.
        {
            //Debug.Log("colllidedwithnpc=true");
            catpause = 0;
            colora = 255;

        }

        if (secondstowait >= catpause) //do the countdown
        {
            Color tempcolor = colorkeeper.material.color;
            
            catpause += Time.deltaTime;
            colora = colora- 50f*Time.deltaTime;

            tempcolor.a = colora;


            colorkeeper.material.color= tempcolor;
           // colorkeeper.color = new Color(1, 200, 39, 255);
        }

        if (secondstowait <= catpause)
        {
            gameObject.SetActive(false);
        }

    }
}
