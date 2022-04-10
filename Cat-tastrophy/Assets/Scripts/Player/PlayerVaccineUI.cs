using UnityEngine;
using UnityEngine.UI;

public class PlayerVaccineUI : MonoBehaviour
{
    [SerializeField] private Sprite vaccine1;
    [SerializeField] private Sprite vaccine2;
    [SerializeField] private Sprite vaccine3;
    [SerializeField] private Sprite vaccine4;
    [SerializeField] private Sprite vaccine5;
    [SerializeField] private Sprite vaccine6;
    [SerializeField] private Sprite vaccine7;
    [SerializeField] private Sprite vaccine8;
    [SerializeField] private Sprite vaccine9;
    [SerializeField] private Sprite vaccine10;
    [SerializeField] private GameObject UIPlayerVaccine;
    private int playerVaccine;
    [SerializeField] private PlayerUIText playerUIText;
    private const int MAX_PLAYER_VACCINE = 10;
    private Image vaccineSpriteImage;
    
    void Start()
    {
        vaccineSpriteImage = UIPlayerVaccine.GetComponent<Image>();
        ResetVaccine();
    }

    public void ResetVaccine()
    {
        playerUIText.StopShowingText();
        vaccineSpriteImage.enabled = true;
        vaccineSpriteImage.sprite = vaccine10;
        playerVaccine = MAX_PLAYER_VACCINE;
    }

    public void AddVaccine(int value)
    {
        playerVaccine += value;
        playerUIText.StopShowingText();
        if (playerVaccine > MAX_PLAYER_VACCINE)
        {
            playerVaccine = MAX_PLAYER_VACCINE;
        }
        ChangeVaccine();
    }
    public bool LostVaccine()
    {
        Debug.Log("Vaccine count: " + playerVaccine);
        playerVaccine--;
        if (playerVaccine < 0)
        {
            playerVaccine = 0;
            playerUIText.AddText("Go to the priest to refill your holy water!");
            playerUIText.StartShowingText(5.0f);
            ChangeVaccine();
            return false;
        }
        else
        {
            transform.GetComponent<AudioSource>().Play();
            ChangeVaccine();
            return true;
        }
    }
    
    private void ChangeVaccine()
    {
        switch (playerVaccine)
        {
            case 0:
                vaccineSpriteImage.enabled = false;
                break;
            case 1:
                vaccineSpriteImage.sprite = vaccine1;
                break;
            case 2:
                vaccineSpriteImage.sprite = vaccine2;
                break;
            case 3:
                vaccineSpriteImage.sprite = vaccine3;
                break;
            case 4:
                vaccineSpriteImage.sprite = vaccine4;
                break;
            case 5:
                vaccineSpriteImage.sprite = vaccine5;
                break;
            case 6:
                vaccineSpriteImage.sprite = vaccine6;
                break;
            case 7:
                vaccineSpriteImage.sprite = vaccine7;
                break;
            case 8:
                vaccineSpriteImage.sprite = vaccine8;
                break;
            case 9:
                vaccineSpriteImage.sprite = vaccine9;
                break;
            case 10:
                vaccineSpriteImage.sprite = vaccine10;
                break;
        }
    }
}
