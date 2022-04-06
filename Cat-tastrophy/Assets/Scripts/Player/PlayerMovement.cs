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
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject waterDropPrefab;
    [SerializeField] private Transform spawnPositionWater;
    [SerializeField] private Transform spawnPositionVaccine;
    [SerializeField] private RectTransform crossHairUI;
    [SerializeField] private GameObject waterParticlesPrefab;
    private float AIM_MAX_DISTANCE = 300.0f;
    private float PLAYER_SPEED = 7.0f;
    private float ARROW_BASE_SPEED = 8.0f;
    private float WATER_BASE_SPEED = 3.0f;
    private Vector2 movementDirection;
    private Vector3 mousePosition;
    private Vector2 movementCoordinates;
    private int MAX_VACCINE_MUNITION = 10;
    private int vaccineMunition;
    private int MAX_WATER_MUNITION = 10;
    private int waterMunition;
    private Vector2 screenCoords;
    private bool refill = false;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
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
    }

    private void OnMove(InputValue value)
    {
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
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Camera.main.farClipPlane;

        GameObject arrow = Instantiate(arrowPrefab, spawnPositionVaccine.position, Quaternion.identity);
        arrow.transform.Rotate(0, 0,90);
        
        Rigidbody2D player = arrow.GetComponent<Rigidbody2D>();
        player.velocity = screenCoords * ARROW_BASE_SPEED;
        arrow.transform.up = new Vector3(player.velocity.x, player.velocity.y, 0.0f);
        Destroy(arrow, 3.0f);
    }
    
    void Aim()
    {
        Vector2 pixelCoords = Mouse.current.position.ReadValue();
        float x = pixelCoords.x / Screen.width;
        float y = pixelCoords.y / Screen.height;
        screenCoords = new Vector2(x, y);
        screenCoords.x = (screenCoords.x - 0.5f) * 2.0f;
        screenCoords.y = (screenCoords.y - 0.5f) * 2.0f;
        screenCoords.Normalize();
        
        crossHairUI.localPosition = screenCoords * AIM_MAX_DISTANCE;
    }
}
