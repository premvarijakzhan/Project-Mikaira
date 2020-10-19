using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public abstract class Weapon : MonoBehaviour
{

    
    [Header("Basic Information")]
    public string weaponName;
    public Sprite weaponImage;
    public WeaponType weaponType;
    public FireSelector fireSelector;
    public DamageType damageType;
    [Header("Variables/Values")]
    public int damage;
    public int armorDamage;
    public int maxAmmo;
    public float critChance;
    public float timeBetweenShots;
    [Header("Visuals")]
    public GameObject bulletPrefab;
    public GameObject muzzleFlashEffect;
    public Transform muzzle;
    [Header("Misc.")]
    public bool isSuppressed;
    public bool hasAutoReload;
    public bool isOneHanded;
    public int AmmoCount
    {
        get { return ammoCount; }
        set
        {
            ammoCount = value;
            if (ammoCount <= 0)
            {
                overheadText.text = "Press 'R' to Reload!";
                ammoCount = 0;
            }
                
            if (ammoCount > maxAmmo)
                ammoCount = maxAmmo;
            UIManager.instance.UpdateAmmoUI(AmmoCount);
        }
    }
    #region ENUMS
    public enum WeaponType
    {
        Melee,
        Ranged,
        Special,
        AreaDamage
    }
    


    public enum FireSelector
    {
        SemiAutomatic,
        Automatic,
        ReloadAutomatic
    }
    
    public enum DamageType
    {
        SingleShot,
        Explosive,
        StraightShot,
        MultiShot,
        ChainShot
    }
    #endregion
    #region PRIVATE VARIABLES
    private int ammoCount;
    private int damageDone;
    private float shotTime;
    private GameObject leftArm;
    private TextMeshProUGUI overheadText;
    #endregion
    private void Start()
    {
        AmmoCount = maxAmmo;
        UIManager.instance.UpdateAmmoUI(AmmoCount);
        UIManager.instance.UpdateMaxAmmoUI(maxAmmo);
        overheadText = GameObject.Find("PlayerOverheadText").GetComponent<TextMeshProUGUI>();
        if(isOneHanded)
        {
            leftArm = GameObject.Find("LeftArm");
            leftArm.SetActive(false);
        }
    }
    public float DoDamage()
    {
        float randomValue = UnityEngine.Random.Range(0,100);
        if (randomValue <= critChance)
        {
            Debug.Log("CRITICAL HIT!");
        }
        else
        {
            Debug.Log("NORMAL HIT caused : " + damage + " damage!");
        }
        //Will need to write out Damage code here to broadcast
        return damageDone;
    }
    public void FireRangedWeapon(Transform muzzle)
    {
        if (AmmoCount > 0)
        {
            if (Time.time >= shotTime)
            {
                switch (damageType)
                {
                    case DamageType.SingleShot:
                        {
                            StartCoroutine(MuzzleFlash());
                            Instantiate(bulletPrefab, muzzle.position, Quaternion.Euler(muzzle.rotation.x, muzzle.rotation.y, muzzle.rotation.z - 90));
                            shotTime = Time.time + timeBetweenShots;
                        }
                        break;
                    case DamageType.StraightShot:
                        {
                            //sniper fire
                        }
                        break;
                    case DamageType.MultiShot:
                        {
                            StartCoroutine(MuzzleFlash());
                            int rand = UnityEngine.Random.Range(1, 6);
                            for (var i = 0;i < rand; i++){
                                var b = Instantiate(bulletPrefab, muzzle.position, Quaternion.Euler(muzzle.rotation.x, muzzle.rotation.y, muzzle.rotation.z - 90));
                            }
                            //Play Sound Effect
                            AdjustAmmo(-1);
                            shotTime = Time.time + timeBetweenShots;
                        }
                        break;
                    case DamageType.Explosive:
                        {
                            //explosive shot
                        }
                        break;
                    case DamageType.ChainShot:
                        {
                            //chained shot
                        }
                        break;
                    default:
                        Debug.Log("FierRangedWeapon Switch defaulted with value of: " +damageType);
                        break;
                }
            }
        }
        else
        {
            Debug.Log("No Ammo! Reload!");
            
        }
    }

    
    private void AdjustAmmo(int amount)
    {
        AmmoCount += amount;
    }

    public void ReloadWeapon()
    {
        AmmoCount = maxAmmo;
        StartCoroutine(ClearText());
        Debug.Log("Reloaded to Full Ammo!");
    }

    public void ReloadWeapon(int ammount)
    {
        ammoCount += ammount;
        StartCoroutine(ClearText());
        Debug.Log("Reloaded " + ammount + " rounds!");
    }

    public void IncreaseCritChance(float critInc)
    {
        critChance += critInc;
        if (critChance > 100.0f)
            critChance = 100.0f;
    }

    IEnumerator MuzzleFlash()
    {
        muzzleFlashEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        muzzleFlashEffect.SetActive(false);
    }
    IEnumerator ClearText()
    {
        Debug.Log("Clearing Text");
        yield return new WaitForSeconds(0.25f);
        overheadText.text = "";
    }
}
