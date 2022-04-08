using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public Highscore highScore;
    public GameObject highScoreScreen;
    public VampireManager vampireManager;
    public Text scoreText;
    public Text deathCauseText;

    private PlayerMovement _movement;
    private PlayerShooting _shooting;
    private PlayerPauseMenu _pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        _movement = gameObject.GetComponent<PlayerMovement>();
        _shooting = gameObject.GetComponent<PlayerShooting>();
        _pauseMenu = gameObject.GetComponent<PlayerPauseMenu>();
    }

    public void LostGame(bool playerGameOver)
    {
        gameOverCanvas.SetActive(true);
        _movement.enabled = false;
        _shooting.enabled = false;
        _pauseMenu.enabled = false;
        if (playerGameOver)
        {
            deathCauseText.text = "You got killed!";
        }
        else
        {
            deathCauseText.text = "You lost all your villagers!";
        }
 
        int i = vampireManager.GetNumberVampiresKilled();
        int number = highScore.AddScore(i);
        scoreText.text = i.ToString() + " Vampires";
        RefreshHighscores(number);
    }

    //Gets all the scores and puts them into the correct place
    private void RefreshHighscores(int number)
    {
        Image[] scorePlaces = highScoreScreen.GetComponentsInChildren<Image>();
        if(number >= 0)
        {
            scorePlaces[number].color = Color.red;
        }

        for (int i = 0; i < highScore.scoreList.Length; i++)
        {
            Text[] scoreText = scorePlaces[i].GetComponentsInChildren<Text>();
            scoreText[1].text = highScore.scoreList[i].ToString() + " Punkte";
        }
    }

    public void OnNewGame()
    {
        int i = Random.Range(1, 3);
        SceneManager.LoadScene(i, LoadSceneMode.Single);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
