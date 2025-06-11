using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float lifetime = 5;
    
    void DestroyController()
    {
        Destroy(gameObject);
    }
    
    void Start()
    {
        Invoke("DestroyController", lifetime);
    }
}