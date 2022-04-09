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
    private bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        _movement = gameObject.GetComponent<PlayerMovement>();
        _shooting = gameObject.GetComponent<PlayerShooting>();
        _pauseMenu = gameObject.GetComponent<PlayerPauseMenu>();
    }

    public void LostGame(bool playerGameOver)
    {
        Debug.Log("Game over");
        if(_isGameOver) { return; }
        _isGameOver = true;
        gameOverCanvas.SetActive(true);
        _movement.SetPauseMenuOpen(_isGameOver);
        float _fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
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
 
        int l = vampireManager.GetNumberVampiresKilled();
        int number = highScore.AddScore(l);
        scoreText.text = l.ToString() + " Vampires";
        for (int i = 0; i < highScore.scoreList.Length; i++)
        {
            int score = highScore.scoreList[i];
            PlayerPrefs.SetInt(i.ToString(), score);
        }
        PlayerPrefs.Save();
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
