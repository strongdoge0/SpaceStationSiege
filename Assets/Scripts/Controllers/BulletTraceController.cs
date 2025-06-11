using UnityEngine;

public class BulletTraceController : MonoBehaviour
{
    public Vector3 hitPoint;
    public float movementSpeed = 60.0f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, hitPoint, movementSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, hitPoint) < 0.5f) {
            Destroy(gameObject);
        }
    }

}