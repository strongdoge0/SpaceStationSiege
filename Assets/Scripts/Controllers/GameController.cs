using System.Collections.Generic;
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

    bool isPlaying = false;

    public float time = 0;
    public int score = 0;

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

    public void GenerateScene(int minSize, int maxSize, int meteorCount, int omegaCount, int betaCount, int alphaCount)
    {
        List<Transform> units = new List<Transform>();

        while (omegaCount > 0)
        {
            float x = Random.Range(minSize, maxSize);
            float y = Random.Range(minSize, maxSize);
            float z = Random.Range(minSize, maxSize);
            Vector3 position = new Vector3(x, y, z);
            if (Vector3.Distance(stationUnit.transform.position, position) > 15)
            {
                if (Vector3.Distance(playerController.transform.position, position) > 15)
                {
                    bool isValidPos = true;
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (Vector3.Distance(units[i].position, position) < 15)
                        {
                            isValidPos = false;
                            break;
                        }
                    }
                    if (isValidPos)
                    {
                        Transform transform = GameObject.Instantiate(enemyOmegaPrefab, position, Quaternion.identity, scene).transform;
                        transform.gameObject.name = "Omega " + omegaCount;
                        units.Add(transform);
                        omegaCount--;
                    }
                }
            }
        }

        while (betaCount > 0)
        {
            float x = Random.Range(minSize, maxSize);
            float y = Random.Range(minSize, maxSize);
            float z = Random.Range(minSize, maxSize);
            Vector3 position = new Vector3(x, y, z);
            if (Vector3.Distance(stationUnit.transform.position, position) > 15)
            {
                if (Vector3.Distance(playerController.transform.position, position) > 15)
                {
                    bool isValidPos = true;
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (Vector3.Distance(units[i].position, position) < 15)
                        {
                            isValidPos = false;
                            break;
                        }
                    }
                    if (isValidPos)
                    {
                        Transform transform = GameObject.Instantiate(enemyBetaPrefab, position, Quaternion.identity, scene).transform;
                        transform.gameObject.name = "Beta " + betaCount;
                        units.Add(transform);
                        betaCount--;
                    }
                }
            }
        }

        while (alphaCount > 0)
        {
            float x = Random.Range(minSize, maxSize);
            float y = Random.Range(minSize, maxSize);
            float z = Random.Range(minSize, maxSize);
            Vector3 position = new Vector3(x, y, z);
            if (Vector3.Distance(stationUnit.transform.position, position) > 15)
            {
                if (Vector3.Distance(playerController.transform.position, position) > 15)
                {
                    bool isValidPos = true;
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (Vector3.Distance(units[i].position, position) < 15)
                        {
                            isValidPos = false;
                            break;
                        }
                    }
                    if (isValidPos)
                    {
                        Transform transform = GameObject.Instantiate(enemyAlphaPrefab, position, Quaternion.identity, scene).transform;
                        transform.gameObject.name = "Alpha " + alphaCount;
                        units.Add(transform);
                        alphaCount--;
                    }
                }
            }
        }

        while (meteorCount > 0)
        {
            float x = Random.Range(minSize, maxSize);
            float y = Random.Range(minSize, maxSize);
            float z = Random.Range(minSize, maxSize);
            Vector3 position = new Vector3(x, y, z);
            if (Vector3.Distance(stationUnit.transform.position, position) > 15)
            {
                if (Vector3.Distance(playerController.transform.position, position) > 15)
                {
                    bool isValidPos = true;
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (Vector3.Distance(units[i].position, position) < 15)
                        {
                            isValidPos = false;
                            break;
                        }
                    }
                    if (isValidPos)
                    {
                        GameObject go = GameObject.Instantiate(meteorPrefab, position, Quaternion.identity, scene);
                        go.name = "Meteor " + meteorCount;
                        meteorCount--;
                    }
                }
            }
        }
    }

    public void StartGame(int difficulty = 2)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stationUnit = GameObject.Instantiate(stationPrefab, new Vector3(), Quaternion.identity, scene).GetComponent<Unit>();
        stationUnit.curHealth = stationUnit.maxHealth;
        playerController = GameObject.Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity, scene).GetComponent<PlayerController>();
        playerController.cameraController = cameraController;
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

        int size = 75;
        if (difficulty == 1)
        {
            size = 150;
        }
        else if (difficulty == 2)
        {
            size = 300;
        }
        GenerateScene(-size, size, meteorCount, omegaCount, betaCount, alphaCount);

        scene.gameObject.SetActive(true);

        uIController.ShowGameUI();
        isPlaying = true;
        time = 0;
        score = 0;
    }

    public void Die(string message = "")
    {
        SetPaused(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        uIController.ShowGameEnd();
        isPlaying = false;
    }

    public void EndGame()
    {
        for (int i = 0; i < scene.childCount; i++)
        {
            GameObject.Destroy(scene.GetChild(i).gameObject);
        }
        scene.gameObject.SetActive(false);
        isPlaying = false;
    }

    void Start()
    {

    }

    void Update()
    {
        if (isPlaying)
        {
            time += Time.deltaTime;
            uIController.DrawTime(time);
            uIController.DrawScore(score);
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
}
