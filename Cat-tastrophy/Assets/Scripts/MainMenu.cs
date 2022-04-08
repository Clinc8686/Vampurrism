using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject highScoreScreen;
    public GameObject creditsScreen;
    public int[] gameSceneNames;
    public Highscore highScore;
    public Text scoreToAdd;

    // Start is called before the first frame update
    void Start()
    {
        RefreshHighscores();
    }

    //Gets all the scores and puts them into the correct place
    private void RefreshHighscores()
    {
        Image[] scorePlaces = highScoreScreen.GetComponentsInChildren<Image>();

        for (int i = 0; i < highScore.scoreList.Length; i++)
        {
            Text[] scoreText = scorePlaces[i + 1].GetComponentsInChildren<Text>();
            scoreText[1].text = highScore.scoreList[i].ToString() + " Punkte";
        }
        highScoreScreen.SetActive(false);
    }

    //Only for testing if the score-Method works
    public void AddScore()
    {
        highScore.AddScore(int.Parse(scoreToAdd.text));
        RefreshHighscores();
    }

    //Close the game when clicked on "Beenden"
    public void OnCloseClicked()
    {
        Application.Quit();
    }

    //Start a new game when clicked on "Spiel starten"
    public void OnStartClicked()
    {
        int i = Random.Range(1, gameSceneNames.Length+1);
        SceneManager.LoadScene(i, LoadSceneMode.Single);
    }

    //Show the Highscore List when clicked on "Highscore"
    public void OnHighscoreClicked()
    {
        highScoreScreen.SetActive(true);
    }

    //Hide the Highscore List when clicked on "Zurück"
    public void OnHighscoreBackClicked()
    {
        highScoreScreen.SetActive(false);
    }

    //Show the Credits when clicked on "Credits"
    public void OnCreditsClicked()
    {
        creditsScreen.SetActive(true);
    }

    //Hide the Credits when clicked on "Zurück"
    public void OnCreditsBackClicked()
    {
        creditsScreen.SetActive(false);
    }
}
