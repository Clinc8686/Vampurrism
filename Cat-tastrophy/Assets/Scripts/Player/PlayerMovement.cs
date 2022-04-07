using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
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
    private Animator animator;
    private float AIM_MAX_DISTANCE = 2.0f;
    private float PLAYER_SPEED = 5.0f;
    private float VACCINE_BASE_SPEED = 15.0f;
    private float WATER_BASE_SPEED = 12.0f;
    private Vector2 movementDirection;
    private Vector2 movementCoordinates;
    private int MAX_VACCINE_MUNITION = 10;
    private int vaccineMunition;
    private int MAX_WATER_MUNITION = 10;
    private int waterMunition;
    private Vector2 screenCoords;
    private bool refill = false;
    private bool rotated = false;
    private bool moving = false;

    void Start()
    {
        animator = body.GetComponent<Animator>();
        vaccineMunition = MAX_VACCINE_MUNITION;
        waterMunition = MAX_WATER_MUNITION;
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
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("WaterWell"))
        {
            refill = false;
        }
    }

    private void OnInteraction()
    {
        if (refill)
        {
            if (waterMunition < 10)
            {
                waterMunition = 10;
                
            }
            if (vaccineMunition < 10)
            {
                vaccineMunition = 10;
            }
        }
    }

    private void ShootWater()
    {
        GameObject waterParticles = Instantiate(waterParticlesPrefab, spawnPositionWater.position, Quaternion.identity);
        waterParticles.transform.Rotate(0, 0, 90);
        waterParticles.transform.right = new Vector3((screenCoords.x*WATER_BASE_SPEED), (screenCoords.y*WATER_BASE_SPEED), 0.0f);
        Destroy(waterParticles, 15.0f);
    }

    private void ShootVaccine()
    {
        GameObject arrow = Instantiate(vaccinePrefab, spawnPositionVaccine.position, Quaternion.identity);
        arrow.transform.Rotate(0, 0,90);
        
        //---
        Rigidbody2D arrowRigidbody2D = arrow.GetComponent<Rigidbody2D>();
        arrowRigidbody2D.velocity = screenCoords * VACCINE_BASE_SPEED;
        arrow.transform.up = new Vector3(arrowRigidbody2D.velocity.x, arrowRigidbody2D.velocity.y, 0.0f);
        //Rigidbody2D player = arrow.GetComponent<Rigidbody2D>();
        //player.velocity = screenCoords * VACCINE_BASE_SPEED;
        //arrow.transform.up = new Vector3(player.velocity.x, player.velocity.y, 0.0f);
        Destroy(arrow, 3.0f);
    }
    
    void Aim()
    {
        Vector2 pixelCoords = Mouse.current.position.ReadValue();
        float x = pixelCoords.x / Screen.width;
        float y = pixelCoords.y / Screen.height;
        Vector2 scCoords = new Vector2(x, y);
        screenCoords.x = (scCoords.x - 0.5f) * 2.0f;
        screenCoords.y = (scCoords.y - 0.5f) * 2.0f;
        Debug.Log("vorher x:" + screenCoords.x + " y:" + screenCoords.y);
        screenCoords.Normalize();
        Debug.Log("nachher x:" + screenCoords.x + " y:" + screenCoords.y);
        crossHairUI.localPosition = screenCoords * AIM_MAX_DISTANCE;
    }
}
