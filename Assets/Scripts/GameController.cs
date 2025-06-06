using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIController uIController;
    public CameraController cameraController;
    public Unit stationUnit;
    public PlayerController playerController;
    public Transform scene;

    public GameObject stationPrefab;
    public GameObject playerPrefab;
    public GameObject meteorPrefab;
    public GameObject enemyAlphaPrefab;
    public GameObject enemyBetaPrefab;
    public GameObject enemyOmegaPrefab;

    public Vector3 playerSpawnPoint = new Vector3(8, 6, 50);

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

    public void StartGame(int difficulty = 0)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stationUnit = GameObject.Instantiate(stationPrefab, new Vector3(), Quaternion.identity, scene).GetComponent<Unit>();
        stationUnit.curHealth = stationUnit.maxHealth;
        playerController = GameObject.Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity, scene).GetComponent<PlayerController>();
        cameraController.InitializeTarget(playerController);

        int meteorCount = 100;
        if (difficulty == 1)
        {
            meteorCount = 200;
        }
        else if (difficulty == 2)
        {
            meteorCount = 400;
        }

        for (int i = 0; i < meteorCount; i++)
        {
            float x = Random.Range(-100, 100);
            float y = Random.Range(-100, 100);
            float z = Random.Range(-100, 100);
            Vector3 position = new Vector3(x, y, z);
            if (Vector3.Distance(stationUnit.transform.position, position) > 15)
            {
                if (Vector3.Distance(playerController.transform.position, position) > 15)
                {
                    GameObject.Instantiate(meteorPrefab, position, Quaternion.identity, scene);
                }
            }
        }

        int omegaCount = 3;
        int betaCount = 2;
        int alphaCount = 1;
        if (difficulty == 1)
        {
            omegaCount = 6;
            betaCount = 4;
            alphaCount = 2;
        }
        else if (difficulty == 2)
        {
            omegaCount = 12;
            betaCount = 8;
            alphaCount = 4;
        }

        for (int i = 0; i < omegaCount; i++)
        {
            float x = Random.Range(-100, 100);
            float y = Random.Range(-100, 100);
            float z = Random.Range(-100, 100);
            Vector3 position = new Vector3(x, y, z);
            if (Vector3.Distance(stationUnit.transform.position, position) > 15)
            {
                if (Vector3.Distance(playerController.transform.position, position) > 15)
                {
                    GameObject.Instantiate(enemyOmegaPrefab, position, Quaternion.identity, scene);
                }
            }
        }

        for (int i = 0; i < betaCount; i++)
        {
            float x = Random.Range(-100, 100);
            float y = Random.Range(-100, 100);
            float z = Random.Range(-100, 100);
            Vector3 position = new Vector3(x, y, z);
            if (Vector3.Distance(stationUnit.transform.position, position) > 15)
            {
                if (Vector3.Distance(playerController.transform.position, position) > 15)
                {
                    GameObject.Instantiate(enemyBetaPrefab, position, Quaternion.identity, scene);
                }
            }
        }

        for (int i = 0; i < alphaCount; i++)
        {
            float x = Random.Range(-100, 100);
            float y = Random.Range(-100, 100);
            float z = Random.Range(-100, 100);
            Vector3 position = new Vector3(x, y, z);
            if (Vector3.Distance(stationUnit.transform.position, position) > 15)
            {
                if (Vector3.Distance(playerController.transform.position, position) > 15)
                {
                    GameObject.Instantiate(enemyAlphaPrefab, position, Quaternion.identity, scene);
                }
            }
        }

        scene.gameObject.SetActive(true);

        uIController.ShowGameUI();
    }

    public void EndGame()
    {
        for (int i = 0; i < scene.childCount; i++)
        {
            GameObject.Destroy(scene.GetChild(i).gameObject);
        }
        scene.gameObject.SetActive(false);
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
