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
    private float PLAYER_SPEED = 7.0f;
    private float AIM_MAX_DISTANCE = 5.0f;
    private float ARROW_BASE_SPEED = 4.0f;
    private Vector2 movementDirection;
    private Vector3 mousePosition;
    private Vector2 movementCoordinates;
    private int munition = 10;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
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
        if (munition > 0)
        {
            ShootVaccine();
            munition--;
        }
    }

    private void OnSprayWater(InputValue value)
    {
        ShootWater();
    }

    private void OnInteraction(InputValue value)
    {
        
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
        arrow.GetComponent<Rigidbody2D>().velocity = worldPoint * ARROW_BASE_SPEED;
        arrow.transform.Rotate(0,0,Mathf.Atan2(worldPoint.y, worldPoint.x) * Mathf.Rad2Deg);
        Destroy(arrow, 3.0f);
    }
    
    void Aim()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Camera.main.farClipPlane;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 movementDir = new Vector2(worldPoint.x, worldPoint.y);
        movementDir.Normalize();
        
        crossHair.transform.localPosition = movementDir * AIM_MAX_DISTANCE;

    }
}
