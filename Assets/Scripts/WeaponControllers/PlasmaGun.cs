using UnityEngine;

public class PlasmaGun : WeaponController
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
    }
}
