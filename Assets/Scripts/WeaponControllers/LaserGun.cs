using UnityEngine;

public class LaserGun : WeaponController
{
    public override void Shot()
    {
        base.Shot();
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        UpdateCooldown();
        if (!isEnabled) return;
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }
}
