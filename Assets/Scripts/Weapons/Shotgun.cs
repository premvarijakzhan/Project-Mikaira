using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    private SpriteRenderer sr;
    private WeaponType type = WeaponType.Ranged;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = weaponImage;
        muzzle = gameObject.transform.GetChild(0);
    }
}
