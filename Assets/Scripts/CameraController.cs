using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    
    void Start()
    {
        
    }


    void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position + target.forward * -6 + target.up * 2.2f; //+ new Vector3(0, 2.2f, -3);
            transform.forward = target.forward;
        }
    }
}
