using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2.2f, -6);
    public UIController uIController;
    private Unit _targetInCrosshair = null;


    public float normalFOV = 75;
    public float zoomedFOV = 35;
    private float _currentFOV = 75;
    public float zoomSpeed = 5.0f;


    void Start()
    {

    }


    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + target.forward * offset.z + target.up * offset.y;
            transform.forward = target.forward;

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<Unit>())
                {
                    _targetInCrosshair = hit.collider.GetComponent<Unit>();
                }
            }
            uIController.DrawTargetStatusBar(_targetInCrosshair);

            if (Input.GetMouseButton(1))
            {
                _currentFOV = Mathf.Lerp(_currentFOV, zoomedFOV, Time.deltaTime * zoomSpeed);
            }
            else
            {
                _currentFOV = Mathf.Lerp(_currentFOV, normalFOV, Time.deltaTime * zoomSpeed);
            }
            Camera.main.fieldOfView = _currentFOV;
        }
        else
        {
            _currentFOV = normalFOV;
        }
    }
}
