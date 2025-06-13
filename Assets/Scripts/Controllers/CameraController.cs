using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController _target;
    public Vector3 offset = new Vector3(0, 2.2f, 6);
    public GameController gameController;
    private Unit _targetInCrosshair = null;
    public Unit target { get { return _targetInCrosshair; } }
    private bool _targetLock = false;

    public float cameraMovementSpeed = 30;

    public float normalFOV = 75;
    public float zoomedFOV = 35;
    private float _currentFOV = 75;
    public float zoomSpeed = 5.0f;

    void Start()
    {

    }

    public void InitializeTarget(PlayerController target)
    {
        _target = target;
        transform.position = target.transform.position - target.transform.forward * (offset.z + target.speed / 3) + target.transform.up * offset.y;
        _targetLock = false;
    }

    void Update()
    {
        if (_target != null)
        {
            Vector3 targetPosition = _target.transform.position - _target.transform.forward * (offset.z + _target.speed / 3) + _target.transform.up * offset.y;
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraMovementSpeed * Time.deltaTime);
            //transform.position = target.position + target.forward * offset.z + target.up * offset.y;
            transform.forward = _target.transform.forward;

            if (_targetInCrosshair != null)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _targetLock = !_targetLock;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    _targetInCrosshair = null;
                    _targetLock = false;
                }
                
                
            }

            if (_targetLock && _targetInCrosshair == null)
            {
                _targetLock = false;
            }

            if (!_targetLock)
            {
                //int layer
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.GetComponent<Unit>())
                    {
                        _targetInCrosshair = hit.collider.GetComponent<Unit>();
                    }
                }
            }
            gameController.uIController.DrawTargetStatusBar(_targetInCrosshair, _targetLock);

            if (Input.GetMouseButton(1))
            {
                _currentFOV = Mathf.Lerp(_currentFOV, zoomedFOV, Time.deltaTime * zoomSpeed);
            }
            else
            {
                _currentFOV = Mathf.Lerp(_currentFOV, normalFOV, Time.deltaTime * zoomSpeed);
            }
            Camera.main.fieldOfView = _currentFOV;

            gameController.uIController.DrawPlayerStatusBar();
        }
        else
        {
            _currentFOV = normalFOV;
            gameController.uIController.DrawPlayerStatusBar();
        }
    }
}
