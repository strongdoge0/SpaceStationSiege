using UnityEngine;

public class Unit : MonoBehaviour
{
    public new string name = "Unit";
    public int maxHealth = 100;
    private int _health = 100;
    public int curHealth
    {
        get { return _health; }
        set
        {
            _health = value;
            _health = Mathf.Clamp(_health, 0, maxHealth);
            if (_health == 0)
            {
                Die();
            }
        }
    }

    public GameObject explosionPrefab;

    public virtual void Die()
    {
        if (explosionPrefab != null)
        {
            GameObject.Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public virtual void OnTakeDamage(int damage)
    {

    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        OnTakeDamage(damage);
    }

    void Start()
    {
        _health = Mathf.Clamp(_health, 0, maxHealth);
    }


    void Update()
    {

    }
}
