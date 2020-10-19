using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AimWeapon : MonoBehaviour
{
    public CharacterController2D controller;
    private Quaternion rotation;
    public bool isDebug = false;
    private GameObject wh;
    private bool hasWeapon = false;
    private Weapon equippedWeapon;
    private void Start()
    {
        wh = GameObject.Find("WeaponHolder");
        if (wh.transform.childCount > 0)
        {
            Debug.Log("Has a Weapon!");
            hasWeapon = true;
            equippedWeapon = wh.transform.GetChild(0).gameObject.GetComponent<Weapon>();
        }
        else
        {
            Debug.Log("Has NO Weapon!");
            hasWeapon = false;
            equippedWeapon = null;
            wh.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        WeaponAim();
        if (hasWeapon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack(equippedWeapon);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                equippedWeapon.ReloadWeapon();
            }
        }
    }

    void WeaponAim()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //convert pixels to Unity units to get its position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;                          //do math things to get the degrees needed to rotate
        if (controller.m_FacingRight)                                                                 //check which direction we're facing to make sure gun points the right way
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        else
            rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        transform.rotation = rotation;                                                                //set weapon/arms rotation to point towards mouse
    }

    void Attack(Weapon wep)
    {
        switch (wep.weaponType)
        {
            case (Weapon.WeaponType.Melee):
                {
                    //do melee stuff
                }
                break;
            case (Weapon.WeaponType.Ranged):
                {
                    Debug.Log("We are attacking with a ranged weapon!");
                    equippedWeapon.FireRangedWeapon(wep.muzzle);
                }
                break;
            case (Weapon.WeaponType.Special):
                {
                    //do special stuff
                }
                break;
            case (Weapon.WeaponType.AreaDamage):
                {
                    //do AoE stuff
                }
                break;
            default:
                Debug.Log("Attack Weapon Type switch statement defaulted after getting a value of: " + equippedWeapon.weaponType);
                break;
        }




    }
}
