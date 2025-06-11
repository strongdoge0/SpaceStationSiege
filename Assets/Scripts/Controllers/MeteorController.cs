using UnityEngine;

public class MeteorController : Unit
{
    public override void Die()
    {
        if (explosionPrefab != null)
        {
            Transform explosionTransform = GameObject.Instantiate(explosionPrefab, transform.position, transform.rotation).transform;
            explosionTransform.localScale = transform.localScale;
        }
        Destroy(gameObject);
    }
}