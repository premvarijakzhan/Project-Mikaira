﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    private Rigidbody2D rb;
    Vector3 shootDirection;
    public GameObject contactExplosion;
    public Weapon parentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        parentWeapon = GameObject.Find("WeaponHolder").transform.GetChild(0).GetComponent<Weapon>();
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        Debug.Log("Bullet Spawned!");
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(shootDirection.x * bulletSpeed * Time.deltaTime, shootDirection.y * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered on "+ other.gameObject.name);
        if(other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            GameObject _ex = Instantiate(contactExplosion, gameObject.transform.position, Quaternion.identity);
            parentWeapon.DoDamage();
            Destroy(_ex, 2f);
        }
        
    }
}
