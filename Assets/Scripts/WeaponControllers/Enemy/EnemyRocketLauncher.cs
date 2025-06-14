using UnityEngine;

public class EnemyRocketLauncher : WeaponController
{
    public GameObject rocketPrefab;
    public Transform rocketSpawnTransform;
    public AudioSource audioSource;

    public float maxShotToPlayerDistance = 150.0f;

    public Unit ignoreUnit;

    public override void Shot()
    {
        if (Time.timeScale == 0) return;
        if (curCooldown > 0) return;
        GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gameController.playerController == null && gameController.stationUnit == null)
        {
            return;
        }
        _curCooldown = cooldown;

        RocketController rocket = GameObject.Instantiate(rocketPrefab, rocketSpawnTransform.position, transform.rotation, gameController.scene).GetComponent<RocketController>();
        rocket.damage = (int)((float)rocket.damage * gameController.enemyDamageMultiplier);
        rocket.ignoreUnit = ignoreUnit;
        Transform target;

        //Debug.Log(Vector3.Distance(transform.position, gameController.playerController.transform.position));

        if (Vector3.Distance(transform.position, gameController.playerController.transform.position) <= maxShotToPlayerDistance)
        {
            target = gameController.playerController.transform;
            rocket.lifetime = 5;
        }
        else
        {
            target = gameController.stationUnit.transform;
            rocket.lifetime = 1000;
        }

        rocket.InitializeTarget(target);
        audioSource.Play();

    }

    void Start()
    {
        Initialize();
    }


    void Update()
    {
        UpdateCooldown();
    }
}
