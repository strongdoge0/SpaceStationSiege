using UnityEngine;

public class PlasmaGun : WeaponController
{
    public GameObject hitPrefab;
    public GameObject bulletTracePrefab;

    public int damagePerShot = 5;
    public int shotsCount = 5;
    public float timePerShot = 0.25f;

    public AudioSource audioSource;

    private bool _shoting = false;
    private float _curShot = 0;
    private float _curTime = 0;

    public override void Shot()
    {
        _curAmount--;
        Vector3 offset = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        //Debug.Log(offset);
        BulletTraceController bulletTrace = GameObject.Instantiate(bulletTracePrefab, transform.position , transform.rotation).GetComponent<BulletTraceController>();
        CameraController cameraController = Camera.main.GetComponent<CameraController>();

        Vector3 hitPoint;
        RaycastHit hit;
        Ray ray = new Ray(cameraController.transform.position + offset, cameraController.transform.forward);
        if (Physics.Raycast(ray, out hit, 100))
        {
            hitPoint = hit.point;
            Transform hitTransform = GameObject.Instantiate(hitPrefab, hit.point, Quaternion.identity).transform;
            hitTransform.up = hit.normal;
            hitTransform.position += hitTransform.up * 0.1f;

            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit)
            {
                int damage = (int)(damagePerShot * cameraController.gameController.playerDamageMultiplier);
                unit.TakeDamage(damage);
            }
        }
        else
        {
            hitPoint = cameraController.transform.position + offset + cameraController.transform.forward * 100;
        }
        bulletTrace.hitPoint = hitPoint;
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        UpdateCooldown();
        if (!isEnabled) return;

        if (_shoting)
        {
            if (_curShot == shotsCount)
            {
                _shoting = false;
            }
            _curTime += Time.deltaTime;
            if (_curTime >= timePerShot)
            {
                _curTime -= timePerShot;
                _curShot++;
                Shot();
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.timeScale == 0) return;
                if (curCooldown > 0) return;
                if (amount - shotsCount >= 0)
                {
                    _curCooldown = cooldown;
                    _shoting = true;
                    _curShot = 0;
                    _curTime = 0;
                    audioSource.Play();
                }
            }
        }
    }
}
