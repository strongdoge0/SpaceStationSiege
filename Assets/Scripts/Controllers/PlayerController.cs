using System.Linq;
using UnityEngine;

public class PlayerController : Unit
{
    //private Unit _unit;
    public CameraController cameraController;

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
    public WeaponController[] weaponControllers = new WeaponController[3];
    public int currentWeapon = 1;

    void Start()
    {
        //_unit = GetComponent<Unit>();
        SelectWeapon(currentWeapon);
    }

    public override void OnTakeDamage(int damage)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().score -= damage;
    }

    public override void Die()
    {
        cameraController.gameController.Die("Корабль игрока уничтожен");
        base.Die();
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter");
        Unit unit = collision.gameObject.GetComponent<Unit>();
        if (unit)
        {
            //Debug.Log("OnCollisionEnter with unit " + unit.name);
            unit.TakeDamage((int)_currentSpeed);
            TakeDamage((int)_currentSpeed);
        }
    }

    void SelectWeapon(int weaponIndex)
    {
        currentWeapon = Mathf.Clamp(currentWeapon, 0, weaponControllers.Length - 1);

        for (int i = 0; i < weaponControllers.Length; i++)
        {
            if (i != currentWeapon)
            {
                weaponControllers[i].isEnabled = false;
            }
            else
            {
                weaponControllers[i].isEnabled = true;
            }
        }
    }

    void Update()
    {
        //if (_unit.curHealth <= 0) return;
        if (curHealth <= 0) return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentWeapon++;
            SelectWeapon(currentWeapon);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            currentWeapon--;
            SelectWeapon(currentWeapon);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
            SelectWeapon(currentWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
            SelectWeapon(currentWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
            SelectWeapon(currentWeapon);
        }



        /*if (currentWeapon < 0)
        {
            currentWeapon = weaponControllers.Length - 1;
        }
        else if (currentWeapon > weaponControllers.Length - 1)
        {
            currentWeapon = 0;
        }*/

        /*if (Input.GetMouseButtonDown(0))
        {
            weaponControllers[currentWeapon].Shot();
        }*/

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

        if (_currentSpeed < maxMovementSpeed - 4)
        {
            if (hyperdrive.isPlaying)
            {
                hyperdrive.Stop(true);
            }
        }
        else
        {
            if (!hyperdrive.isPlaying)
            {
                hyperdrive.Play(true);
            }
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
