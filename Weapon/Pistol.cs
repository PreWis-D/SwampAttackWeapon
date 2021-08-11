using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private Bullet _bullet;

    public override void Shoot(Transform shootPoint)
    {
        Instantiate(_bullet, shootPoint.position, Quaternion.identity);
    }
}
