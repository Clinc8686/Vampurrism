using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private GameObject body;
    [SerializeField] private PlayerVaccineUI playerVaccineUI;
    [SerializeField] private PlayerLifeUI playerLifeUI;
    [SerializeField] private PlayerShooting playerShooting;
    private GameObject well;
    private List<GameObject> foodList;
    private GameObject priest;
    private int foodEnterCounter = 0;
    private Vector2 movementDirection;
    private Vector2 movementCoordinates;
    private float PLAYER_SPEED = 15.0f;
    private int MAX_WATER_MUNITION = 1000005;
    private int waterMunition;
    private bool refill = false;
    private bool rebless = false;
    private bool pickUpFood = false;
    private bool rotated = false;
    private bool moving = false;
    public Animator animator;
    private DateTime oldTime;

    void Start()
    {
        animator = body.GetComponent<Animator>();
        waterMunition = MAX_WATER_MUNITION;
    }

    void FixedUpdate()
    {
        playerRB.velocity += movementCoordinates * Time.deltaTime;
        Vector3 playerLocalScale = transform.localScale;
        if (rotated)
        {
            transform.localScale = new Vector3(-1, playerLocalScale.y, playerLocalScale.z);
        } else
        {
            transform.localScale = new Vector3(1, playerLocalScale.y, playerLocalScale.z);
        }

        if (moving)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void OnMove(InputValue value)
    { 
        Vector2 input = value.Get<Vector2>();
        if (input.x > 0)
        {
            rotated = true;
        } else if (input.x < 0)
        {
            rotated = false;
        }

        if (Vector2.Distance(input, new Vector2(0,0)) > 0)
        {
            moving = true;
        }
        else
        {
            moving = false; 
        }
        movementDirection = value.Get<Vector2>();
        playerRB.velocity = new Vector2(movementDirection.normalized.x, movementDirection.normalized.y) * PLAYER_SPEED;
    }

    private void OnVaccinate(InputValue value)
    {
        if (playerVaccineUI.LostVaccine())
        {
            playerShooting.ShootVaccine();
        }
    }

    private void OnSprayWater(InputValue value)
    {
        if (waterMunition > 0)
        {
            playerShooting.ShootWater();
            waterMunition--;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("WaterWell"))
        {
            refill = true;
            well = col.gameObject;
        }
        else if(col.CompareTag("Priest"))
        {
            rebless = true;
            priest = col.gameObject;
        }
        else if (col.CompareTag("Food"))
        {
            pickUpFood = true;
            if(!foodList.Contains(col.gameObject))
            {
                foodList.Add(col.gameObject);
            }
            foodEnterCounter++;
        }
        
        if (col.CompareTag("Enemy"))
        {
            playerLifeUI.LostLife();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        DateTime now = DateTime.Now;
        TimeSpan difference = now.Subtract(oldTime);
        if (difference.TotalSeconds > 1)
        {
            if (col.CompareTag("Enemy"))
            {
                playerLifeUI.LostLife();
            }
            oldTime = DateTime.Now;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("WaterWell"))
        {
            refill = false;
            well = null;
        }
        else if (col.CompareTag("Priest"))
        {
            rebless = false;
            priest = null;
        }
        else if (col.CompareTag("Food"))
        {
            foodList.Remove(col.gameObject);
            if(foodList.Count <= 0)
            {
                pickUpFood = false;
            }
        }
    }

    private void OnInteraction()
    {
        if (refill)
        {
            bool canRefill = well.GetComponent<Well>().GetBlessedWater();
            if(!canRefill) { return; }
            waterMunition = 10;
            playerVaccineUI.ResetVaccine();
        }
        else if (rebless)
        {
            priest.GetComponent<PriestBlessing>().BlessWell();
        }
        else if (pickUpFood)
        {
            int heal = foodList[0].GetComponent<Food>().GetRegenHealth();
            foodList[0].GetComponent<Food>().OnPickUpFood();
            playerLifeUI.AddLife(heal);
        }
    }


}
