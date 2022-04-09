using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject highScoreScreen;
    public GameObject creditsScreen;
    public GameObject howToPlayScreen;
    public GameObject optionsScreen;
    public int[] gameSceneNames;
    public Highscore highScore;
    public Text scoreToAdd;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < highScore.scoreList.Length; i++)
        {
            int score = PlayerPrefs.GetInt(i.ToString());
            highScore.scoreList[i] = score;
        }
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

    //Close the game when clicked on "Beenden"
    public void OnCloseClicked()
    {
        StartCoroutine(TestQuit());
    }

    IEnumerator TestQuit()
    {
        yield return new WaitForSeconds(1);
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

    //Show the Credits when clicked on "Credits"
    public void OnCreditsClicked()
    {
        creditsScreen.SetActive(true);
    }

    public void OnHowToPlayClicked()
    {
        howToPlayScreen.SetActive(true);
    }

    public void OnOptionsClicked()
    {
        optionsScreen.SetActive(true);
    }

    //Hide the Credits when clicked on "Zurück"
    public void OnBackClicked()
    {
        creditsScreen.SetActive(false);
        highScoreScreen.SetActive(false);
        howToPlayScreen.SetActive(false);
        optionsScreen.SetActive(false);
    }
}
