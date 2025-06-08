using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int amount = 0;
    public float culldown = 2.0f;
    private float _curCulldown;
    public float curCulldown { get { return _curCulldown; } }

    public virtual void Shot()
    {
        if (curCulldown > 0) return;
        if (amount == 0) return;
        _curCulldown = culldown;
        if (amount - 1 >= 0)
        {
            amount--;
        }
    }

    void Start()
    {
        _curCulldown = culldown;
    }

    void Update()
    {
        if (curCulldown > 0)
        {
            _curCulldown -= Time.deltaTime;
        }
    }
}
