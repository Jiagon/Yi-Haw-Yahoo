using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurret : Placeable
{
    [HideInInspector] public float damage = 50f;
    [HideInInspector] public float nextAttack = 2f;
    public GameObject projectile;

    List<GameObject> enemies;
    float timer;

    void Start()
    {
        enemies = new List<GameObject>();
    }

    void Update()
    {
        if (enemies.Count > 0)
        {
            timer += Time.deltaTime;
            if(timer > nextAttack)
            {
                timer = 0f;
                List<GameObject> dead = new List<GameObject>();
                foreach(GameObject enemy in enemies)
                {
                    Debug.Log("Health: " + enemy.GetComponent<EnemyScript>().health);
                    enemy.GetComponent<EnemyScript>().TakeDamage(damage);
                    if (enemy.GetComponent<EnemyScript>().health <= 0)
                        dead.Add(enemy);
                }
                foreach (GameObject enemy in dead)
                    enemies.Remove(enemy);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            enemies.Add(collision.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (enemies.Contains(collision.gameObject))
            enemies.Remove(collision.gameObject);
    }
}
