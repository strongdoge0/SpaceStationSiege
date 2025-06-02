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
        }
    }

    void Die()
    {

    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
