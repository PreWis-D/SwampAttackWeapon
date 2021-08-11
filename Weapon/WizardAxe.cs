using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAxe : Weapon
{
    [SerializeField] private ShockWave _shockWave;

    public override void Shoot(Transform shootPoint)
    {
        Instantiate(_shockWave, shootPoint.position, Quaternion.identity);
    }
}
