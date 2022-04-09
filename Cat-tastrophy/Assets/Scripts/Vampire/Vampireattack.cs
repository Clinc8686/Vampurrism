using UnityEngine;
using System.Collections;

public class Vampireattack : MonoBehaviour
{
    public int attackOverTimeCooldown = 2;
    GameObject villagermanager;
    GameObject Vampiremanager;
    PlayerLifeUI playerLifeUI;  //Von Mario
    bool collidedWithNpc = false;
    float catpause = 0;
    private Vector3 npcPos;
    private bool _attackedPlayer = false;
    // Update is called once per frame
    void Update()
    {
        if(collidedWithNpc == true)
        {
            waitforseconds(2);
        }
    }
    private void Start()
    {
        villagermanager = GameObject.Find("VillagerManager");
        Vampiremanager = GameObject.Find("EnemyManager");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_attackedPlayer)
        {
            Debug.Log("KEep colliding with a player");
            playerLifeUI.LostLife();
            _attackedPlayer = true;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackOverTimeCooldown);
        _attackedPlayer = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Stopped colliding with a player");
            _attackedPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with a player");
            GameObject temp = collision.gameObject.transform.parent.gameObject;
            GameObject temp2 = temp.gameObject.transform.parent.gameObject;
            playerLifeUI = temp2.GetComponentInChildren<PlayerLifeUI>();
            playerLifeUI.LostLife();
            _attackedPlayer = true;
            StartCoroutine(AttackCooldown());
        }
        else*/ if (collision.gameObject.tag == "Villager"&& collidedWithNpc == false)
        {
            //Debug.Log("turning a cat into a vampire muahahahah"); // 0 = normal, 1= impf


            collision.gameObject.SetActive(false);
            this.GetComponent<Animator>().SetBool("fighting", true);

            gameObject.GetComponent<VampireMovement>().collidedWithNpc = true;
            collision.gameObject.GetComponent<VillagerMovement>().collidedWithVampire = true;

            collidedWithNpc = true;
            npcPos = collision.gameObject.GetComponent<Transform>().position;
        }
        else if (collision.gameObject.tag == "imm") //"immune", for now isn't complete
        {
            Debug.Log("Oh no, my bite doesn't work");

            collision.gameObject.SetActive(false);
            this.GetComponent<Animator>().SetBool("fighting", true);

            gameObject.GetComponent<VampireMovement>().collidedWithNpc = true;
            collision.gameObject.GetComponent<VillagerMovement>().collidedWithVampire = true;
            //add the logic to let them go seperate ways.
        }
    }


    private void waitforseconds(int seconds)
    {
        if (collidedWithNpc == true && seconds <= catpause) //prepare the countdown, something collided.
        {
            Debug.Log("colllidedwithnpc=true");
            catpause = 0;
        }

        if (seconds >= catpause && collidedWithNpc == true) //do the countdown
        {
            catpause += Time.deltaTime;
        }

        if (seconds <= catpause && collidedWithNpc == true)
        {
            collidedWithNpc = false;
            Vampiremanager.GetComponent<VampireManager>().Addvampire(1, npcPos);

            villagermanager.GetComponent<VillagerManager>().VillagerDied();
            //Vampiremanager.GetComponent<VampireManager>().numberVampires += 1;
            GetComponent<Animator>().SetBool("fighting", false); // test
        }
    }
}
