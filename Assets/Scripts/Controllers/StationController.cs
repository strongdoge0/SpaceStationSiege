using Unity.Mathematics;
using UnityEngine;

public class StationController : Unit
{
    public float movementSpeed = 10;
    public float angularSpeed = 10;

    void Start()
    {

    }

    public override void OnTakeDamage(int damage)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().score -= damage;
    }

    public override void Die()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Die("Командная станция уничтожена");
        base.Die();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(), movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion.identity, angularSpeed * Time.deltaTime);
    }
}
