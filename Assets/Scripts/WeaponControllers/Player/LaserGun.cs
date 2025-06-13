using UnityEngine;

public class LaserGun : WeaponController
{
    public GameObject hitPrefab;
    public GameObject leserTracePrefab;

    public float minPrepareTime = 0.25f;
    public float maxPrepareTime = 2.5f;
    private float _prepareTime = 0;
    public float prepareTime { get { return _prepareTime; } }
    private bool _isPreparingStart = false;
    private bool _isPreparingEnd = true;

    public int damagePerSecond = 10;

    public AudioSource prepareAudioSource;
    public AudioSource lowShotAudioSource;
    public AudioSource highShotAudioSource;

    public ParticleSystem implosionParticle;
    public ParticleSystem beamParticle;

    public override void Shot()
    {
        BulletTraceController bulletTrace = GameObject.Instantiate(leserTracePrefab, transform.position, transform.rotation).GetComponent<BulletTraceController>();
        CameraController cameraController = Camera.main.GetComponent<CameraController>();

        Vector3 hitPoint;
        RaycastHit hit;
        Ray ray = new Ray(cameraController.transform.position, cameraController.transform.forward);
        if (Physics.Raycast(ray, out hit, 200))
        {
            hitPoint = hit.point;
            Transform hitTransform = GameObject.Instantiate(hitPrefab, hit.point, Quaternion.identity).transform;
            hitTransform.up = hit.normal;
            hitTransform.localScale *= _prepareTime;
            hitTransform.position += hitTransform.up * 0.1f;

            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit)
            {
                int damage = (int)(damagePerSecond * _prepareTime * cameraController.gameController.playerDamageMultiplier);
                unit.TakeDamage(damage);
            }
        }
        else
        {
            hitPoint = cameraController.transform.position + cameraController.transform.forward * 200;
        }
        bulletTrace.hitPoint = hitPoint;
    }

    void Start()
    {
        if (implosionParticle.isPlaying)
        {
            implosionParticle.Stop(false);
        }
        beamParticle.Stop(false);
        cooldown = maxPrepareTime;
        Initialize();
    }

    void Update()
    {
        UpdateCooldown();
        if (!isEnabled) return;

        if (_isPreparingStart)
        {
            if (!implosionParticle.isPlaying)
            {
                implosionParticle.Play(false);
            }
            beamParticle.Play(false);
            _prepareTime += Time.deltaTime;
            _prepareTime = Mathf.Clamp(_prepareTime, 0, maxPrepareTime);
            if (!Input.GetMouseButton(0))
            {
                _isPreparingEnd = true;
            }

            if (_isPreparingEnd && _prepareTime >= minPrepareTime)
            {
                _curCooldown = _prepareTime;
                cooldown = _prepareTime;
                _isPreparingStart = false;
                Shot();
                if (_prepareTime <= minPrepareTime + 0.5f)
                {
                    lowShotAudioSource.Play();
                }
                else
                {
                    highShotAudioSource.Play();
                }
                prepareAudioSource.Stop();
                _isPreparingStart = false;
                _prepareTime = 0;
            }

        }
        else
        {
            if (implosionParticle.isPlaying)
            {
                implosionParticle.Stop(false);
            }
            beamParticle.Stop(false);
            if (Input.GetMouseButton(0) && _prepareTime == 0 && _isPreparingEnd)
            {
                if (curCooldown <= 0)
                {
                    _isPreparingStart = true;
                    _isPreparingEnd = false;
                    prepareAudioSource.Play();
                }
            }
        }
    }
}
