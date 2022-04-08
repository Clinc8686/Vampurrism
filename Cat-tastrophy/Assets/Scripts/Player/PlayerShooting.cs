using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject vaccinePrefab;
    [SerializeField] private Transform spawnPositionWater;
    [SerializeField] private Transform spawnPositionVaccine;
    [SerializeField] private GameObject waterParticlesPrefab;
    [SerializeField] private RectTransform crossHairUI;
    [SerializeField] private PlayerMovement playerMovement;
    private float AIM_MAX_DISTANCE = 200.0f;
    private float VACCINE_BASE_SPEED = 20.0f;
    private float WATER_BASE_SPEED = 20.0f;
    public Vector2 mouseDirectionFromVaccinePosition;
    public Vector2 mouseDirectionFromWaterPosition;

    void Start()
    {
        //playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    
    private void LateUpdate()
    {
        Aim();
    }
    
    public void ShootWater()
    {
        playerMovement.animator.SetTrigger("Shooting");
        GameObject waterParticles = Instantiate(waterParticlesPrefab, spawnPositionWater.position, Quaternion.identity);
        waterParticles.transform.Rotate(0, 0, 90);
        Vector3 velocity = new Vector3((mouseDirectionFromWaterPosition.x*WATER_BASE_SPEED), (mouseDirectionFromWaterPosition.y*WATER_BASE_SPEED), 0.0f);
        waterParticles.transform.right = velocity;
        Destroy(waterParticles, 15.0f);
    }

    public void ShootVaccine()
    {
        playerMovement.animator.SetTrigger("Shooting");

        GameObject arrow = Instantiate(vaccinePrefab, spawnPositionVaccine.position, Quaternion.identity);
        arrow.transform.Rotate(0, 0,90);
        
        Rigidbody2D arrowRigidbody2D = arrow.GetComponent<Rigidbody2D>();
        arrowRigidbody2D.velocity = mouseDirectionFromVaccinePosition.normalized * VACCINE_BASE_SPEED;
        arrow.transform.up = new Vector3(arrowRigidbody2D.velocity.x, arrowRigidbody2D.velocity.y, 0.0f);
        Destroy(arrow, 15.0f);
    }
    
    private void Aim()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 positionInScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mouseDirectionFromPosition = mousePosition - positionInScreenSpace;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseDirectionFromVaccinePosition = mousePosition - (Vector2) spawnPositionVaccine.position;
        mouseDirectionFromWaterPosition = mousePosition - (Vector2) spawnPositionWater.position;
        crossHairUI.anchoredPosition = positionInScreenSpace + mouseDirectionFromPosition.normalized * AIM_MAX_DISTANCE;
    }
}
