using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2.2f, -6);
    public UIController uIController;
    private Unit _targetInCrosshair = null;
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
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.GetComponent<Unit>()) {
                    _targetInCrosshair = hit.collider.GetComponent<Unit>();
                }
            }
            uIController.DrawTargetStatusBar(_targetInCrosshair);
        }
    }
}
