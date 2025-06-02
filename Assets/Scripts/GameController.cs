using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIController uIController;
    public CameraController cameraController;
    public PlayerController playerController;
    public Transform scene;

    public bool isPaused
    {
        get
        {
            if (Time.timeScale == 0)
            {
                return true;
            }
            return false;
        }
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            return;
        }
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraController.target = playerController.transform;
        scene.gameObject.SetActive(true);

        uIController.ShowGameUI();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPaused(!isPaused);
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                uIController.ShowGameMenu();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                uIController.HideGameMenu();
            }
        }
    }
}
