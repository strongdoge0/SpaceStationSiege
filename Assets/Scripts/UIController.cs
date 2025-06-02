using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameController gameController;

    public RectTransform mainMenu;
    public RectTransform gameUI;
    public RectTransform gameMenu;


    void Start()
    {

    }

    public void ShowMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
        gameMenu.gameObject.SetActive(false);
    }

    public void ShowGameUI()
    {
        mainMenu.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
        gameMenu.gameObject.SetActive(false);
    }

    public void ShowGameMenu()
    {
        gameMenu.gameObject.SetActive(true);
    }

    public void HideGameMenu()
    {
        gameMenu.gameObject.SetActive(false);
    }

    public void OnNewGameButton()
    {
        ShowGameUI();
        gameController.StartGame();
    }

    public void OnResumeButton()
    {
        gameController.SetPaused(false);
        HideGameMenu();
    }

    public void OnExitToMainMenu()
    {
        ShowMainMenu();
        gameController.SetPaused(true);
    }
    
    public void OnExitButton()
    {
        Application.Quit();
    }

    void Update()
    {

    }
}
