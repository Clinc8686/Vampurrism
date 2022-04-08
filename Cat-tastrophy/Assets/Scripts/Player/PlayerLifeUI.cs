using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeUI : MonoBehaviour
{
    [SerializeField] private Sprite heart1;
    [SerializeField] private Sprite heart2;
    [SerializeField] private Sprite heart3;
    [SerializeField] private Sprite heart4;
    [SerializeField] private Sprite heart5;
    [SerializeField] private Sprite heart6;
    [SerializeField] private Sprite heart7;
    [SerializeField] private GameObject UIPlayerLife;
    private Image lifeSpriteImage;
    private int playerLife;
    private int MAX_PLAYER_LIFE = 7;
    
    
    void Start()
    {
        lifeSpriteImage = UIPlayerLife.GetComponent<Image>();
        ResetLife();
    }

    public void ResetLife()
    {
        lifeSpriteImage.enabled = true;
        lifeSpriteImage.sprite = heart7;
        playerLife = MAX_PLAYER_LIFE;
    }

    public void AddLife(int value)
    {
        if (playerLife > MAX_PLAYER_LIFE)
        {
            playerLife = MAX_PLAYER_LIFE;
        }
        ChangeLife();
    }
    public bool LostLife()
    {
        playerLife--;
        if (playerLife <= 0)
        {
            playerLife = 0;
            ChangeLife();
            //gameover
            return false;
        }
        else
        {
            ChangeLife();
            return true;
        }
    }
    
    private void ChangeLife()
    {
        switch (playerLife)
        {
            case 0:
                lifeSpriteImage.enabled = false;
                break;
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
