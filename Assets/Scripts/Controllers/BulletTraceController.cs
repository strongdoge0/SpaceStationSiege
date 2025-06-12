using UnityEngine;

public class BulletTraceController : MonoBehaviour
{
    public Vector3 hitPoint;
    public float movementSpeed = 60.0f;
    public float timeToDestroy = 0;

    void DestroyTracer()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, hitPoint, movementSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, hitPoint) < 0.5f)
        {
            if (timeToDestroy == 0)
            {
                DestroyTracer();
            }
            else
            {
                Invoke("DestroyTracer", timeToDestroy);
            }
        }
    }

}