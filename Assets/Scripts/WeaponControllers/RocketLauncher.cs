using UnityEngine;

public class RocketLauncher : WeaponController
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
