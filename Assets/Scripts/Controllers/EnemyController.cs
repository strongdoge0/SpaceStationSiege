using UnityEngine;

public class EnemyController : Unit
{
    Vector3 _orbitCenter = Vector3.zero;
    public float movementSpeed = 10;
    public float heightChangeSpeed  = 0.1f;
    private float _angularSpeed = 1f;

    private float _radius;
    private float _currentAngle;

    private float _targetHeight;
    private float _currentHeight;

    private Vector3 _prevPos;

    void ChangeTargetHeight()
    {
        _targetHeight = Random.Range(-150, 150);
        Invoke("ChangeTargetHeight", 15 * Random.Range(1, 10));

    }

    public override void OnTakeDamage(int damage)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().score += damage;
    }

    public override void Die()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().score += maxHealth;
        base.Die();
    }

    void OnCollisionEnter(Collision collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();
        if (unit)
        {
            if (unit.tag == "Enemy") {
                unit.TakeDamage((int)movementSpeed);
                TakeDamage((int)movementSpeed);
            }
        }
    }

    void Start()
    {
        Vector3 offset = transform.position - _orbitCenter;
        _radius = offset.magnitude;
        _angularSpeed = movementSpeed / _radius;
        if (Random.value < 0.5f)
        {
            _angularSpeed *= -1;
            //Debug.Log("_angularSpeed = -1");
        }
        _currentAngle = Mathf.Atan2(offset.z, offset.x);
        _targetHeight = transform.position.y;
        _currentHeight = _targetHeight;
        Invoke("ChangeTargetHeight", 60 * Random.Range(0, 1));
        _prevPos = transform.position;
    }

    void Update()
    {
        if (curHealth <= 0) return;
        
        _currentAngle += _angularSpeed * Time.deltaTime;

        //_currentHeight = Mathf.Lerp(_currentHeight, _targetHeight, heightChangeSpeed  * Time.deltaTime);
        _currentHeight = Mathf.MoveTowards(_currentHeight, _targetHeight, heightChangeSpeed * Time.deltaTime);
        
        float x = _orbitCenter.x + Mathf.Cos(_currentAngle) * _radius;
        float z = _orbitCenter.z + Mathf.Sin(_currentAngle) * _radius;
        Vector3 currentPos = new Vector3(x, _currentHeight, z);

        /*float nextAngle = _currentAngle + 0.01f;
        float nextX = _orbitCenter.x + Mathf.Cos(nextAngle) * _radius;
        float nextZ = _orbitCenter.z + Mathf.Sin(nextAngle) * _radius;
        Vector3 nextPos = new Vector3(nextX, _currentHeight, nextZ);*/

        Vector3 direction = (currentPos - _prevPos).normalized;
        transform.position = currentPos;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        _prevPos = currentPos;
    }
}
