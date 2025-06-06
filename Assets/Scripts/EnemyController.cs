using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 _orbitCenter = Vector3.zero;
    public float movementSpeed = 10;
    float _angularSpeed = 1f;

    float _radius;
    float _currentAngle;

    void Start()
    {
        Vector3 offset = transform.position - _orbitCenter;
        _radius = offset.magnitude;
        //_angularSpeed = Mathf.Lerp(0.1789f * (movementSpeed / 10), 0.003777f * (movementSpeed / 10), Vector3.Distance(transform.position, _orbitCenter) / 300);
        _angularSpeed = movementSpeed / _radius;

        _currentAngle = Mathf.Atan2(offset.z, offset.x);
    }

    void Update()
    {
        _currentAngle += _angularSpeed * Time.deltaTime;

        float x = _orbitCenter.x + Mathf.Cos(_currentAngle) * _radius;
        float z = _orbitCenter.z + Mathf.Sin(_currentAngle) * _radius;
        Vector3 currentPos = new Vector3(x, transform.position.y, z);

        float nextAngle = _currentAngle + 0.01f;
        float nextX = _orbitCenter.x + Mathf.Cos(nextAngle) * _radius;
        float nextZ = _orbitCenter.z + Mathf.Sin(nextAngle) * _radius;
        Vector3 nextPos = new Vector3(nextX, transform.position.y, nextZ);

        Vector3 direction = (nextPos - currentPos).normalized;
        transform.position = currentPos;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
