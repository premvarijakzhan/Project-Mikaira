using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI playerHealth;
    public TextMeshProUGUI playerArmor;
    public TextMeshProUGUI playerAmmo;
    public TextMeshProUGUI playerMaxAmmo;
    public TextMeshProUGUI playerOverHeadText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void UpdateHealthUI(int health)
    {
        playerHealth.text = health.ToString();
    }
    public void UpdateArmorUI(int armor)
    {
        playerArmor.text = armor.ToString();
    }
    public void UpdateAmmoUI(int ammo)
    {
        playerAmmo.text = ammo.ToString();
    }
    public void UpdateMaxAmmoUI(int ammo)
    {
        playerMaxAmmo.text = ammo.ToString();
    }
}
