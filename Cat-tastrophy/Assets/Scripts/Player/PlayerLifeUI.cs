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
    private int playerLife;
    private int MAX_PLAYER_LIFE = 7;
    private Image lifeSpriteImage;
    
    void Start()
    {
        lifeSpriteImage = UIPlayerLife.GetComponent<Image>();
        resetLife();
    }

    public void resetLife()
    {
        lifeSpriteImage.sprite = heart7;
        playerLife = MAX_PLAYER_LIFE;
    }

    public void addLife(int value)
    {
        if (playerLife > MAX_PLAYER_LIFE)
        {
            playerLife = MAX_PLAYER_LIFE;
        }
        changeLife();
    }
    public bool lostLife()
    {
        if (playerLife <= 1)
        {
            //gameover
            return false;
        }
        else
        {
            playerLife--;
            changeLife();
            return true;
        }
    }
    
    private void changeLife()
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
