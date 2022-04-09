using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerPauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject optionsScreen;

    private bool _isMenuActive = false;
    private float _fixedDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        this._fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPauseMenu(InputValue value)
    {
        _isMenuActive = !_isMenuActive;
        OpenCloseMenu();
    }

    private void OpenCloseMenu()
    {
        pauseCanvas.SetActive(_isMenuActive);
        if (_isMenuActive)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        Time.fixedDeltaTime = this._fixedDeltaTime * Time.timeScale;
    }

    public void OnBackToMainMenuClicked()
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void OnBackToGameClicked()
    {
        Debug.Log("Button clicked");
        _isMenuActive = !_isMenuActive;
        OpenCloseMenu();
    }

    public void OnOptionsClicked()
    {
        optionsScreen.SetActive(true);
    }

    //Hide the Credits when clicked on "Zurück"
    public void OnBackClicked()
    {
        optionsScreen.SetActive(false);
    }
}
