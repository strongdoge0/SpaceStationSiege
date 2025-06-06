using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float maxMovementSpeed = 15;
    public float minMovementSpeed = 5;
    public float movementSpeed = 10;
    public float accelerationSpeed = 5;
    private float _currentSpeed = 5;

    public float speed { get { return _currentSpeed; } }

    public float mouseSensitivity = 30;
    public float angularSpeed = 10;

    private Vector2 _rotation;
    private Vector2 _currentRotation;

    public AudioSource audioSource;

    public ParticleSystem hyperdrive;

    public ParticleSystem mainFlamethrower;
    //public ParticleSystem bottomLeftFlamethrower;
    //public ParticleSystem bottomRightFlamethrower;
    public ParticleSystem topLeftFlamethrower;
    public ParticleSystem topRightFlamethrower;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, maxMovementSpeed, accelerationSpeed * Time.deltaTime);
            /*hyperdrive.Play(true);
            topLeftFlamethrower.Play();
            topRightFlamethrower.Play();
            mainFlamethrower.Play();*/
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, minMovementSpeed, accelerationSpeed * Time.deltaTime);
            /*hyperdrive.Stop(true);
            topLeftFlamethrower.Stop();
            topRightFlamethrower.Stop();
            mainFlamethrower.Stop();*/
        }
        else
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, movementSpeed, accelerationSpeed * Time.deltaTime);
            /*hyperdrive.Stop(true);
            topLeftFlamethrower.Stop();
            topRightFlamethrower.Stop();
            mainFlamethrower.Play();*/
        }

        if (_currentSpeed < movementSpeed - 3)
        {
            topLeftFlamethrower.Stop();
            topRightFlamethrower.Stop();
            mainFlamethrower.Stop();
        }
        else if (_currentSpeed < maxMovementSpeed - 3)
        {
            topLeftFlamethrower.Stop();
            topRightFlamethrower.Stop();
            mainFlamethrower.Play();
        }
        else 
        {
            topLeftFlamethrower.Play();
            topRightFlamethrower.Play();
            mainFlamethrower.Play();
        }

        if (_currentSpeed <= maxMovementSpeed - 3)
        {
            hyperdrive.Stop(true);
        }
        else
        {
            hyperdrive.Play(true);
        }

        _rotation.x += Input.mousePositionDelta.x * mouseSensitivity * Time.deltaTime;
        _rotation.y -= Input.mousePositionDelta.y * mouseSensitivity * Time.deltaTime;
        if (_rotation.y < -89)
        {
            _rotation.y = -89;
        }
        else if (_rotation.y > 89)
        {
            _rotation.y = 89;
        }
        _currentRotation.x = Mathf.Lerp(_currentRotation.x, _rotation.x, angularSpeed * Time.deltaTime);
        _currentRotation.y = Mathf.Lerp(_currentRotation.y, _rotation.y, angularSpeed * Time.deltaTime);

        transform.eulerAngles = new Vector3(_currentRotation.y, _currentRotation.x, 0);
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;

        audioSource.pitch = Mathf.Lerp(0.7f, 1.5f, _currentSpeed / maxMovementSpeed);
    }
}
