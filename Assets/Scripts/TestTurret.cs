﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurret : Placeable
{
    public float nextAttack = 2f;
    public float radius = 20f;
    public GameObject projectile;
    public GameObject bottom;
    public GameObject turnable;

    public EnemyManager eManager;
    List<GameObject> enemies;
    float timer;

    protected override void Start()
    {
        base.Start();
        baseDamage = 10;
        damage = baseDamage;
        enemies = new List<GameObject>();
    }

    override protected void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if(enemies.Count > 0)
            FaceTarget(enemies[0]);
        if (timer > nextAttack)
        {
            timer = 0f;
            FindCloseEnemies();
            if (enemies.Count > 0)
            {
                List<GameObject> dead = new List<GameObject>();
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<EnemyScript>().TakeDamage(damage);
                    GetComponent<AudioSource>().PlayOneShot(GameObject.Find("GameManager").GetComponent<AudioManager>().laser);
                    if (enemy.GetComponent<EnemyScript>().currentHealth <= 0)
                        dead.Add(enemy);
                }
                foreach (GameObject enemy in dead)
                    enemies.Remove(enemy);
            }
        }
    }

    void FindCloseEnemies()
    {
        enemies.Clear();
        foreach (GameObject enemy in eManager.enemies)
        {
            if (Vector3.Magnitude(enemy.transform.position - bottom.transform.position) < radius)
            {
                enemies.Add(enemy);
            }
        }
    }

    void FaceTarget(GameObject target)
    {
        Quaternion newRot = Quaternion.LookRotation(target.transform.position - turnable.transform.position);
        float y = newRot.eulerAngles.y - transform.parent.transform.rotation.y;
        turnable.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
    }
}
