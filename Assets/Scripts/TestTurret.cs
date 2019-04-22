using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurret : Placeable
{
    public int damage = 10;
    public float nextAttack = 2f;
    public float radius = 15f;
    public GameObject projectile;
    public GameObject bottom;

    public EnemyManager eManager;
    List<GameObject> enemies;
    float timer;

    void Start()
    {
        enemies = new List<GameObject>();
    }

    override protected void Update()
    {
        base.Update();
        timer += Time.deltaTime;
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
}
