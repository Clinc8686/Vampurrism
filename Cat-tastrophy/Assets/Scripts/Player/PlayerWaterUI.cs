using UnityEngine;
using UnityEngine.UI;

public class PlayerWaterUI : MonoBehaviour
{
    [SerializeField] private Sprite water1;
    [SerializeField] private Sprite water2;
    [SerializeField] private Sprite water3;
    [SerializeField] private Sprite water4;
    [SerializeField] private Sprite water5;
    [SerializeField] private Sprite water6;
    [SerializeField] private Sprite water7;
    [SerializeField] private Sprite water8;
    [SerializeField] private Sprite water9;
    [SerializeField] private Sprite water10;
    [SerializeField] private Sprite water11;
    [SerializeField] private Sprite water12;
    [SerializeField] private Sprite water13;
    [SerializeField] private Sprite water14;
    [SerializeField] private Sprite water15;
    [SerializeField] private Sprite water16;
    [SerializeField] private Sprite water17;
    [SerializeField] private Sprite water18;
    [SerializeField] private Sprite water19;
    [SerializeField] private Sprite water20;
    [SerializeField] private GameObject UIPlayerWater; 
    
    private int playerWater;
    [SerializeField] private PlayerUIText playerUIText;
    private int MAX_PLAYER_WATER = 20;
    private Image waterSpriteImage;
    private GameObject sparcleeffect;
    
    void Start()
    {
        waterSpriteImage = UIPlayerWater.GetComponent<Image>();
        ResetWater();
        sparcleeffect = GameObject.Find("well particles"); 
        sparcleeffect.SetActive(false);
    }

    public void ResetWater()
    {
        playerUIText.StopShowingText();
        waterSpriteImage.enabled = true;
        waterSpriteImage.sprite = water20;
        playerWater = MAX_PLAYER_WATER;
    }

    public void AddWater(int value)
    {
        playerWater += value;
        sparcleeffect.SetActive(false);
        playerUIText.StopShowingText();
        if (playerWater > MAX_PLAYER_WATER)
        {
            playerWater = MAX_PLAYER_WATER;
        }
        ChangeWater();
    }
    public bool LostWater()
    {
        playerWater--;
        if (playerWater < 0)
        {
            playerWater = 0;
            playerUIText.AddText("Go to the priest to refill your holy Water!");
            playerUIText.StartShowingText(5.0f);
            ChangeWater();
            return false;
        }
        else
        {
            transform.GetComponent<AudioSource>().Play();
            ChangeWater();
            return true;
        }
    }
    
    private void ChangeWater()
    {
        switch (playerWater)
        {
            case 0:
                waterSpriteImage.enabled = false;
                break;
            case 1:
                waterSpriteImage.sprite = water1;
                break;
            case 2:
                waterSpriteImage.sprite = water2;
                break;
            case 3:
                waterSpriteImage.sprite = water3;
                break;
            case 4:
                waterSpriteImage.sprite = water4;
                break;
            case 5:
                waterSpriteImage.sprite = water5;
                break;
            case 6:
                waterSpriteImage.sprite = water6;
                break;
            case 7:
                waterSpriteImage.sprite = water7;
                break;
            case 8:
                waterSpriteImage.sprite = water8;
                break;
            case 9:
                waterSpriteImage.sprite = water9;
                break;
            case 10:
                waterSpriteImage.sprite = water10;
                break;
            case 11:
                waterSpriteImage.sprite = water11;
                break;
            case 12:
                waterSpriteImage.sprite = water12;
                break;
            case 13:
                waterSpriteImage.sprite = water13;
                break;
            case 14:
                waterSpriteImage.sprite = water14;
                break;
            case 15:
                waterSpriteImage.sprite = water15;
                break;
            case 16:
                waterSpriteImage.sprite = water16;
                break;
            case 17:
                waterSpriteImage.sprite = water17;
                break;
            case 18:
                waterSpriteImage.sprite = water18;
                break;
            case 19:
                waterSpriteImage.sprite = water19;
                break;
            case 20:
                waterSpriteImage.sprite = water20;
                break;
        }
    }
}
