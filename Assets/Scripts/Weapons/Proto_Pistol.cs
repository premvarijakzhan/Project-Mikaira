using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class Proto_Pistol : Weapon
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
