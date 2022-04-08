using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerPauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject villagerManager;
    public GameObject enemyManager;

    private bool _isMenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        villagerManager.SetActive(!_isMenuActive);
        enemyManager.SetActive(!_isMenuActive);
    }

    public void OnBackToMainMenuClicked()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void OnBackToGameClicked()
    {
        _isMenuActive = !_isMenuActive;
        OpenCloseMenu();
    }
}
