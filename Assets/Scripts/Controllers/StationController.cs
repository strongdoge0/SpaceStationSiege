using UnityEngine;

public class StationController : Unit
{



    void Start()
    {

    }

    public override void OnTakeDamage(int damage)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().score -= damage;
    }

    public override void Die()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Die();
        base.Die();
    }

    void Update()
    {

    }
}
