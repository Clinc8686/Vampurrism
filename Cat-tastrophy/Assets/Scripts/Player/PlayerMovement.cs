using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private GameObject vaccinePrefab;
    [SerializeField] private Transform spawnPositionWater;
    [SerializeField] private Transform spawnPositionVaccine;
    [SerializeField] private RectTransform crossHairUI;
    [SerializeField] private GameObject waterParticlesPrefab;
    [SerializeField] private GameObject body;
    [SerializeField] private Sprite heart1;
    [SerializeField] private Sprite heart2;
    [SerializeField] private Sprite heart3;
    [SerializeField] private Sprite heart4;
    [SerializeField] private Sprite heart5;
    [SerializeField] private Sprite heart6;
    [SerializeField] private Sprite heart7;
    [SerializeField] private GameObject UIPlayerLife;
    private Animator animator;
    private Image lifeSpriteImage;
    private Vector2 movementDirection;
    private Vector2 movementCoordinates;
    private float AIM_MAX_DISTANCE = 200.0f;
    private float PLAYER_SPEED = 15.0f;
    private float VACCINE_BASE_SPEED = 20.0f;
    private float WATER_BASE_SPEED = 20.0f;
    private int MAX_VACCINE_MUNITION = 1000005;
    private int vaccineMunition;
    private int MAX_WATER_MUNITION = 1000005;
    private int waterMunition;
    private Vector2 mouseDirectionFromVaccinePosition;
    private Vector2 mouseDirectionFromWaterPosition;
    private bool refill = false;
    private bool rebless = false;
    private bool pickUpFood = false;
    private bool rotated = false;
    private bool moving = false;
    private bool shooting = false;
    private int playerLife;
    private GameObject wellOrPriest;

    void Start()
    {
        animator = body.GetComponent<Animator>();
        lifeSpriteImage = UIPlayerLife.GetComponent<Image>();
        lifeSpriteImage.sprite = heart7;
        vaccineMunition = MAX_VACCINE_MUNITION;
        waterMunition = MAX_WATER_MUNITION;
        playerLife = 6;
    }

    private void LateUpdate()
    {
        Aim();
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

        if (shooting)
        {
            
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
        if (vaccineMunition > 0)
        {
            ShootVaccine();
            vaccineMunition--;
        }
    }

    private void OnSprayWater(InputValue value)
    {
        if (waterMunition > 0)
        {
            ShootWater();
            waterMunition--;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("WaterWell"))
        {
            refill = true;
            wellOrPriest = col.gameObject;
        }
        else if(col.CompareTag("Priest"))
        {
            rebless = true;
            wellOrPriest = col.gameObject;
        }
        else if (col.CompareTag("Food"))
        {
            pickUpFood = true;
            wellOrPriest = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("WaterWell"))
        {
            refill = false;
            wellOrPriest = null;
        }
        else if (col.CompareTag("Priest"))
        {
            rebless = false;
            wellOrPriest = null;
        }
        else if (col.CompareTag("Priest"))
        {
            pickUpFood = false;
            wellOrPriest = null;
        }
    }

    private void OnInteraction()
    {
        if (refill)
        {
            bool canRefill = wellOrPriest.GetComponent<Well>().GetBlessedWater();
            if(!canRefill) { return; }
            waterMunition = 10;
            vaccineMunition = 10;
        }
        else if (rebless)
        {
            wellOrPriest.GetComponent<PriestBlessing>().BlessWell();
        }
        else if (pickUpFood)
        {
            //TO DO!!! Regenerated health from food, add to current health
            int heal = wellOrPriest.GetComponent<Food>().GetRegenHealth();
            wellOrPriest.GetComponent<Food>().OnPickUpFood();
            wellOrPriest = null;
            playerLife += heal;
            changeLife();
            if (playerLife > 6)
            {
                playerLife = 6;
            }
        }
    }

    private void ShootWater()
    {
        animator.SetTrigger("Shooting");
        GameObject waterParticles = Instantiate(waterParticlesPrefab, spawnPositionWater.position, Quaternion.identity);
        waterParticles.transform.Rotate(0, 0, 90);
        waterParticles.transform.right = new Vector3((mouseDirectionFromWaterPosition.x*WATER_BASE_SPEED), (mouseDirectionFromWaterPosition.y*WATER_BASE_SPEED), 0.0f);
        Destroy(waterParticles, 15.0f);
    }

    private void ShootVaccine()
    {
        animator.SetTrigger("Shooting");

        GameObject arrow = Instantiate(vaccinePrefab, spawnPositionVaccine.position, Quaternion.identity);
        arrow.transform.Rotate(0, 0,90);
        
        Rigidbody2D arrowRigidbody2D = arrow.GetComponent<Rigidbody2D>();
        arrowRigidbody2D.velocity = mouseDirectionFromVaccinePosition.normalized * VACCINE_BASE_SPEED;
        arrow.transform.up = new Vector3(arrowRigidbody2D.velocity.x, arrowRigidbody2D.velocity.y, 0.0f);
        Destroy(arrow, 15.0f);
    }
    
    void Aim()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 positionInScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mouseDirectionFromPosition = mousePosition - positionInScreenSpace;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseDirectionFromVaccinePosition = mousePosition - (Vector2) spawnPositionVaccine.position;
        mouseDirectionFromWaterPosition = mousePosition - (Vector2) spawnPositionWater.position;
        crossHairUI.anchoredPosition = positionInScreenSpace + mouseDirectionFromPosition.normalized * AIM_MAX_DISTANCE;
    }

    void resetLife()
    {
        lifeSpriteImage.sprite = heart7;
        playerLife = 6;
    }

    void damage()
    {
        playerLife--;
        if (playerLife <= 0)
        {
            //gameover
        }
        else
        {
            changeLife();
        }
    }

    void changeLife()
    {
        switch (playerLife)
        {
            case 1:
                lifeSpriteImage.sprite = heart1;
                break;
            case 2:
                lifeSpriteImage.sprite = heart2;
                break;
            case 3:
                lifeSpriteImage.sprite = heart3;
                break;
            case 4:
                lifeSpriteImage.sprite = heart4;
                break;
            case 5:
                lifeSpriteImage.sprite = heart5;
                break;
            case 6:
                lifeSpriteImage.sprite = heart6;
                break;
            case 7:
                lifeSpriteImage.sprite = heart7;
                break;
        }
    }
}
