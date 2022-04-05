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
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform spawnPositionWater;
    [SerializeField] private Transform spawnPositionVaccine;
    [SerializeField] private RectTransform crossHairUI;
    private float AIM_MAX_DISTANCE = 2.0f;
    private float PLAYER_SPEED = 7.0f;
    private float ARROW_BASE_SPEED = 4.0f;
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
        
    }

    private void ShootVaccine()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Camera.main.farClipPlane;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        GameObject arrow = Instantiate(arrowPrefab, spawnPositionVaccine.position, Quaternion.identity);
        arrow.transform.Rotate(0, 0,270.0f);
        
        //Vector3 crossHairPosition = crossHair.transform.localPosition;
        Rigidbody2D player = arrow.GetComponent<Rigidbody2D>();
        player.velocity = screenCoords * ARROW_BASE_SPEED;
        arrow.transform.up = new Vector3(player.velocity.x, player.velocity.y, 0.0f);
        //Rotate(0,0,Mathf.Atan2(screenCoords, crossHairPosition.x) * Mathf.Rad2Deg);
        Destroy(arrow, 3.0f);
    }
    
    void Aim()
    {
        /*mousePosition = Mouse.current.position.ReadValue();
        Debug.Log("mouse x:" + mousePosition.x + "y:" + mousePosition.y);
        mousePosition.z = Camera.main.farClipPlane;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log("world x:" + worldPoint.x + "y:" + worldPoint.y);
        Vector2 aimDirection = new Vector2(worldPoint.x, worldPoint.y);
        aimDirection = aimDirection + playerRB.position;
        aimDirection.Normalize();
        Debug.Log("aim x:" + aimDirection.x + "y:" + aimDirection.y);
        crossHair.transform.localPosition = (aimDirection) * AIM_MAX_DISTANCE;*/
        
        //Vector2 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        //pos.Normalize();
        //crossHair.transform.localPosition = (pos) * AIM_MAX_DISTANCE;
        
        // Vector2 screenPosition = Mouse.current.position.ReadValue();
        // Debug.Log("aim x:" + screenPosition.x + "y:" + screenPosition.y);
        // Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        // //worldPosition.Normalize();
        // //worldPosition = worldPosition.normalized;
        // Vector2 direction = (worldPosition - playerRB.position).normalized;
        // crossHair.transform.position = playerRB.position + direction * AIM_MAX_DISTANCE;
        
        Vector2 pixelCoords = Mouse.current.position.ReadValue();
        float x = pixelCoords.x / Screen.width;
        float y = pixelCoords.y / Screen.height;
        screenCoords = new Vector2(x, y);
        screenCoords.x = (screenCoords.x - 0.5f) * 2.0f;
        screenCoords.y = (screenCoords.y - 0.5f) * 2.0f;
        screenCoords.Normalize();
        
        crossHairUI.localPosition = screenCoords * AIM_MAX_DISTANCE;

        //Vector3 projectedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //crossHair.transform.localPosition = (projectedMousePosition) * AIM_MAX_DISTANCE;
    }
}
