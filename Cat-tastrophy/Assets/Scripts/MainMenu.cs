using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject highScore;
    public string gameSceneName;
    public Highscore score;
    public Text scoreToAdd;

    // Start is called before the first frame update
    void Start()
    {

        RefreshHighscores();
    }

    //Gets all the scores and puts them into the correct place
    private void RefreshHighscores()
    {
        Image[] scorePlaces = highScore.GetComponentsInChildren<Image>();

        for (int i = 0; i < score.scoreList.Length; i++)
        {
            Text[] scoreText = scorePlaces[i + 1].GetComponentsInChildren<Text>();
            scoreText[1].text = score.scoreList[i].ToString() + " Punkte";
        }
        highScore.SetActive(false);
    }

    //Only for testing if the score-Method works
    public void AddScore()
    {
        score.AddScore(int.Parse(scoreToAdd.text));
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
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }

    //Show the Highscore List when clicked on "Highscore"
    public void OnHighscoreClicked()
    {
        highScore.SetActive(true);
    }

    //Hide the Highscore List when clicked on "Zurück"
    public void OnBackClicked()
    {
        highScore.SetActive(false);
    }
}
