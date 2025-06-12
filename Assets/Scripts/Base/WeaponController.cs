using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public new string name = "Weapon";
    public int maxAmount = 0;
    protected int _curAmount = 0;
    public int amount { get { return _curAmount; } }
    public float cooldown = 2.0f;
    protected float _curCooldown;
    public float curCooldown { get { return _curCooldown; } }

    public bool isEnabled = false;

    public virtual void Shot()
    {
        if (Time.timeScale == 0) return;
        if (curCooldown > 0) return;
        if (amount == 0) return;
        _curCooldown = cooldown;
        if (amount - 1 >= 0)
        {
            _curAmount--;
        }
    }

    public void Initialize()
    {
        _curAmount = maxAmount;
        _curCooldown = cooldown;
    }

    public void UpdateCooldown()
    {
        if (curCooldown > 0)
        {
            _curCooldown -= Time.deltaTime;
            _curCooldown = Mathf.Clamp(_curCooldown, 0, cooldown);
        }
    }
}
