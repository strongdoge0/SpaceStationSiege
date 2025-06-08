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
    public WeaponStatusBar bomb;
    public WeaponStatusBar plasmaGun;
    public WeaponStatusBar laser;


    void Start()
    {

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
                    bomb.DrawAmount(weapon.amount);
                    bomb.DrawCooldowm(weapon.curCulldown, weapon.culldown);
                    bomb.selection.gameObject.SetActive(gameController.playerController.currentWeapon == 0);
                    bomb.icon.color = gameController.playerController.currentWeapon == 0 && weapon.amount != 0 ? Color.white : Color.grey;
                }
                if (gameController.playerController.weaponControllers[1] != null)
                {
                    WeaponController weapon = gameController.playerController.weaponControllers[1];
                    plasmaGun.DrawAmount(weapon.amount);
                    plasmaGun.DrawCooldowm(weapon.curCulldown, weapon.culldown);
                    plasmaGun.selection.gameObject.SetActive(gameController.playerController.currentWeapon == 1);
                    plasmaGun.icon.color = gameController.playerController.currentWeapon == 1 && weapon.amount != 0 ? Color.white : Color.grey;
                }
                if (gameController.playerController.weaponControllers[2] != null)
                {
                    WeaponController weapon = gameController.playerController.weaponControllers[2];
                    laser.DrawAmount(weapon.amount);
                    laser.DrawCooldowm(weapon.curCulldown, weapon.culldown);
                    laser.selection.gameObject.SetActive(gameController.playerController.currentWeapon == 2);
                    laser.icon.color = gameController.playerController.currentWeapon == 2 && weapon.amount != 0 ? Color.white : Color.grey;
                }
            }
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
