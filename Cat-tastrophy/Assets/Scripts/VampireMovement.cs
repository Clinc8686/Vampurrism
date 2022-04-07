using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireMovement : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Target;
    public float VampireSpeed=2f;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    public float offset;
    Rigidbody2D rb2d;
    public float minDistance;
    public bool collidingWithPlayer=false;
    public bool collidedWithNpc=true; // starts standínd and follows afterwards

    private float catpause;
    public int secondstowait=5;

    // remove these Objects

    bool SwitchedCat = false;

    void Start()
    {
        Target = GameObject.Find("Player");

        rb2d = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().gravityScale=0;
    }

    // Update is called once per frame
    void Update()  
    {
        if(angle >= -90f && angle <= 90f )
        {
            if( SwitchedCat == true) //was the cat still flipped?
            {
                SwitchedCat = false;
                flipback(true);
            }
        }
        else
        {
            if (SwitchedCat == false) //was the cat still unflipped? flip
            {
                SwitchedCat = true;
                flipback(false);
            }
        }
        if (collidingWithPlayer == false)
        {
            followPlayer();
        }

    }



    void followPlayer()
    {
        if(collidedWithNpc == true && secondstowait <= catpause) //prepare the countdown, something collided.
        {
            Debug.Log("colllidedwithnpc=true");
            catpause = 0;
            secondstowait = 5;
        }

        if(secondstowait>= catpause && collidedWithNpc == true) //do the countdown
        {
            catpause += Time.deltaTime;
        }

        if (secondstowait <= catpause && collidedWithNpc == true)
        {
            collidedWithNpc = false;
        }

        if (collidedWithNpc == false)
        {
             
            
            targetPos = Target.transform.position;
            thisPos = transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            Debug.Log("rotating");
            angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

            //Find direction
            Vector3 dir = (Target.transform.position - rb2d.transform.position).normalized;
            //Check if we need to follow object then do so 
            if (Vector3.Distance(Target.transform.position, rb2d.transform.position) > minDistance)
            {
                rb2d.MovePosition(rb2d.transform.position + dir * VampireSpeed * Time.fixedDeltaTime);
            }

            Debug.Log("walkin");
        }



    }



    void flipback(bool flipped)
    {

        if(flipped== false) //means it was already flipped and needs to be flipped back
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true ;
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }

        if (flipped == true) //flip upsidedown
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    public void followVillager()
    {
        //not for now
    }

}
