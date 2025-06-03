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

    public float a = 1.0f;
    public float b = 0.5f;
    public float c = 15.0f;

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
            //float distance = Vector3.Distance(transform.position, target.transform.position);
            //distance = Mathf.Clamp(distance, 0.1f, 1f);
            //distance = Mathf.Lerp(a, b, distance / c);
            //targetImage.transform.localScale = new Vector3(distance, distance, 1);
            targetStatusBar.gameObject.SetActive(true);
            targetNameLabel.text = target.name;
            targetFillHealthbar.fillAmount = (float)target.curHealth / (float)target.maxHealth;
        }
        else
        {
            targetStatusBar.gameObject.SetActive(false);
            targetImage.gameObject.SetActive(false);
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

    }
}
