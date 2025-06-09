using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int amount = 0;
    public float cooldown = 2.0f;
    private float _curCooldown;
    public float curCooldown { get { return _curCooldown; } }

    public virtual void Shot()
    {
        if (curCooldown > 0) return;
        if (amount == 0) return;
        _curCooldown = cooldown;
        if (amount - 1 >= 0)
        {
            amount--;
        }
    }

    public void Initialize()
    {
        _curCooldown = cooldown;
    }

    public void UpdateCooldown()
    {
        if (curCooldown > 0)
        {
            _curCooldown -= Time.deltaTime;
        }
    }
}
