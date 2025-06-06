using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameController gameController;

    public RectTransform mainMenu;
    public RectTransform gameUI;
    public RectTransform gameMenu;

    public AudioSource audioSource;

    public RectTransform targetImage;
    public RectTransform targetStatusBar;
    public TMP_Text targetNameLabel;
    public Image targetFillHealthbar;
    public RectTransform distanceLabel;
    public TMP_Text distanceValueLabel;


    public TMP_Text speedValueLabel;



    public Image playerFillHealthbar;

    void Start()
    {

    }

    public void DrawTargetStatusBar(Unit target)
    {
        if (target != null)
        {
            targetImage.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
            if (targetImage.transform.position.z < 0)
            {
                targetImage.gameObject.SetActive(false);
            }
            else
            {
                targetImage.gameObject.SetActive(true);
            }

            targetStatusBar.gameObject.SetActive(true);
            targetNameLabel.text = target.name;
            targetFillHealthbar.fillAmount = (float)target.curHealth / (float)target.maxHealth;

            if (gameController.playerController != null)
            {
                distanceLabel.gameObject.SetActive(true);
                distanceValueLabel.text = (int)(Vector3.Distance(target.transform.position, gameController.playerController.transform.position) * 6.789f) + " м";
            }
        }
        else
        {
            distanceLabel.gameObject.SetActive(false);
            targetStatusBar.gameObject.SetActive(false);
            targetImage.gameObject.SetActive(false);
        }
    }

    public void DrawPlayerStatusBar(Unit target)
    {
        if (target != null)
        {
            playerFillHealthbar.fillAmount = (float)target.curHealth / (float)target.maxHealth;
        }
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
        gameController.SetPaused(false);
        gameController.EndGame();
        ShowMainMenu();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void PlayClickSound()
    {
        audioSource.Play();
    }

    void Update()
    {
        if (gameController.playerController != null)
        {
            speedValueLabel.text = (int)(gameController.playerController.speed * 33.3f) + " км/ч";
        }
    }
}
