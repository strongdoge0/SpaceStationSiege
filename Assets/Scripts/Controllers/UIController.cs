using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameController gameController;

    public RectTransform mainMenu;
    public RectTransform gameUI;
    public RectTransform gameMenu;
    public RectTransform gameEnd;
    public RectTransform gameSettings;

    public AudioSource audioSource;

    public TMP_Text timeValueLabel;
    public TMP_Text scoreValueLabel;
    public TMP_Text gameEndTimeValueLabel;
    public TMP_Text gameEndScoreValueLabel;

    public RectTransform targetImage;
    public RectTransform targetStatusBar;
    public TMP_Text targetNameLabel;
    public Image targetFillHealthbar;
    public RectTransform distanceLabel;
    public TMP_Text distanceValueLabel;

    public TMP_Text speedValueLabel;

    public Image playerFillHealthbar;
    public WeaponStatusBar rocketLauncher;
    public WeaponStatusBar plasmaGun;
    public WeaponStatusBar laserGun;

    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public Slider interfaceVolumeSlider;
    public TMP_Dropdown difficultyDropdown;

    void Start()
    {

    }

    public void DrawTime(float time)
    {
        int totalSeconds = Mathf.FloorToInt(time);
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        string timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        timeValueLabel.text = timeFormatted;
    }

    public void DrawScore(int score)
    {
        scoreValueLabel.text = score.ToString();
    }

    public void DrawTargetStatusBar(Unit target, bool locked)
    {
        if (target != null)
        {
            targetImage.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
            targetImage.GetComponent<Image>().color = locked ? Color.green : Color.white;
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
            if (gameController.playerController != null)
            {
                if (gameController.playerController.weaponControllers[0] != null)
                {
                    WeaponController weapon = gameController.playerController.weaponControllers[0];
                    rocketLauncher.DrawAmount(weapon.amount);
                    rocketLauncher.DrawCooldowm(weapon.curCooldown, weapon.cooldown);
                    rocketLauncher.selection.gameObject.SetActive(gameController.playerController.currentWeapon == 0);
                    rocketLauncher.icon.color = gameController.playerController.currentWeapon == 0 && weapon.amount != 0 ? Color.white : Color.grey;
                }
                if (gameController.playerController.weaponControllers[1] != null)
                {
                    WeaponController weapon = gameController.playerController.weaponControllers[1];
                    plasmaGun.DrawAmount(weapon.amount);
                    plasmaGun.DrawCooldowm(weapon.curCooldown, weapon.cooldown);
                    plasmaGun.selection.gameObject.SetActive(gameController.playerController.currentWeapon == 1);
                    plasmaGun.icon.color = gameController.playerController.currentWeapon == 1 && weapon.amount != 0 ? Color.white : Color.grey;
                }
                if (gameController.playerController.weaponControllers[2] != null)
                {
                    WeaponController weapon = gameController.playerController.weaponControllers[2];
                    laserGun.DrawAmount(weapon.amount);
                    laserGun.DrawCooldowm(weapon.curCooldown, weapon.cooldown);
                    laserGun.selection.gameObject.SetActive(gameController.playerController.currentWeapon == 2);
                    laserGun.icon.color = gameController.playerController.currentWeapon == 2 && weapon.amount != 0 ? Color.white : Color.grey;
                }
            }
        }
        else
        {
            playerFillHealthbar.fillAmount = 0;
        }
    }

    public void ShowMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
        gameMenu.gameObject.SetActive(false);
        gameEnd.gameObject.SetActive(false);
    }

    public void ShowGameUI()
    {
        mainMenu.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
        gameMenu.gameObject.SetActive(false);
        gameEnd.gameObject.SetActive(false);
    }

    public void ShowGameMenu()
    {
        gameMenu.gameObject.SetActive(true);
    }

    public void HideGameMenu()
    {
        gameMenu.gameObject.SetActive(false);
    }

    public void ShowGameEnd()
    {
        mainMenu.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
        gameMenu.gameObject.SetActive(false);
        gameEnd.gameObject.SetActive(true);
        int totalSeconds = Mathf.FloorToInt(gameController.time);
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;
        string timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        gameEndTimeValueLabel.text = timeFormatted;
        gameEndScoreValueLabel.text = gameController.score.ToString();
    }

    public void ShowGameSettings()
    {
        gameSettings.gameObject.SetActive(true);
        // init
    }
    
    public void HideGameSettings()
    {
        gameSettings.gameObject.SetActive(false);
    }

    public void OnGameSettingsApplyButton()
    {
        // save & reload
        HideGameSettings();
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
