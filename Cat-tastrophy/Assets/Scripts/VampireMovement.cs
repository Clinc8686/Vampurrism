using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireMovement : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject PlayerTarget;
    public float VampireSpeed=2f;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    public float offset;

    

    bool SwitchedCat = false;

    void Start()
    {
        PlayerTarget = GameObject.Find("Player");
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

            followPlayer();


        }
        else
        {
            if (SwitchedCat == false) //was the cat still unflipped? flip
            {
                SwitchedCat = true;
                flipback(false);
            }
            followPlayer();

        }


        Debug.Log(angle);
        transform.Translate(VampireSpeed * Time.deltaTime,0.0f, 0.0f );

    }

   void followPlayer()
    {
        targetPos = PlayerTarget.transform.position;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
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


}
