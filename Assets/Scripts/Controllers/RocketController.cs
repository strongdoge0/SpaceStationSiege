using UnityEngine;

public class RocketController : MonoBehaviour
{
    private Transform _target;
    public float lifetime = 5;
    public float movementSpeed = 30;
    public float accelerationSpeed = 5;
    private float _currentSpeed = 5;
    public int damage = 30;
    private float _curTime = 0;

    private Vector3 _prevPos;

    public GameObject explosionPrefab;
    public AudioSource audioSource;

    public void InitializeTarget(Transform target)
    {
        _target = target;
    }

    void OnCollisionEnter(Collision collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();
        if (unit)
        {
            unit.TakeDamage(damage);
            DestroyRocket();
        }
    }

    void Start()
    {

    }

    public void DestroyRocket()
    {
        GameObject.Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Update()
    {
        _curTime += Time.deltaTime;
        if (_curTime >= lifetime)
        {
            DestroyRocket();
        }

        _currentSpeed = Mathf.Lerp(_currentSpeed, movementSpeed, accelerationSpeed * Time.deltaTime);

        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _currentSpeed * Time.deltaTime);

            Vector3 direction = (transform.position - _prevPos).normalized;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
            _prevPos = transform.position;
        }
        else
        {
            transform.position += transform.forward * _currentSpeed * Time.deltaTime;
        }

        audioSource.pitch = Mathf.Lerp(0.7f, 1.5f, _currentSpeed / movementSpeed);
    }
}
