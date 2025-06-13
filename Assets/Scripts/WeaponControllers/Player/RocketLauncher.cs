
using UnityEngine;

public class RocketLauncher : WeaponController
{
    public GameObject rocketPrefab;
    public Transform leftRocketSpawnTransform;
    public Transform rightRocketSpawnTransform;
    private bool _useRightRocketSpawn = true;

    public AudioSource audioSource;

    public override void Shot()
    {
        if (Time.timeScale == 0) return;
        if (curCooldown > 0) return;
        if (amount == 0) return;
        _curCooldown = cooldown;
        if (amount - 1 >= 0)
        {
            _curAmount--;

            Vector3 currentRocketSpawnPoint = _useRightRocketSpawn ? rightRocketSpawnTransform.position : leftRocketSpawnTransform.position;

            RocketController rocket = GameObject.Instantiate(rocketPrefab, currentRocketSpawnPoint, transform.rotation).GetComponent<RocketController>();
            //rocket.transform.forward = Camera.main.transform.forward;
            CameraController cameraController = Camera.main.GetComponent<CameraController>();
            rocket.damage = (int)((float)rocket.damage * cameraController.gameController.playerDamageMultiplier);
            rocket.ignoreUnit = cameraController.gameController.playerController;
            if (cameraController.target != null)
            {
                rocket.InitializeTarget(cameraController.target.transform);
            }
            else
            {
                rocket.InitializeTarget(null);
            }
            audioSource.Play();
            _useRightRocketSpawn = !_useRightRocketSpawn;
        }
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        UpdateCooldown();
        if (!isEnabled) return;
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }
}
