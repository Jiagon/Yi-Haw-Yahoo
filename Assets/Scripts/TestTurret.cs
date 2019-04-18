using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurret : Placeable
{
    public int damage = 10;
    public float nextAttack = 2f;
    float radius = 15f;
    public GameObject projectile;
    public GameObject bottom;

    public EnemyManager eManager;
    List<GameObject> enemies;
    float timer;
    bool isActive = false;

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
        if(isActive) {
            DrawCircle(gameObject,radius, 0.1f);
        } else {
            GetComponent<LineRenderer>().positionCount = 0;
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

    void DrawCircle(GameObject container, float radius, float lineWidth)
    {
        var segments = 360;
        var line = container.GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }
    public void ToggleActive() {
        isActive = !isActive;
    }
}
