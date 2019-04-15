using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject enemyPrefab;
    public uint numEnemies = 0;

    List<GameObject> Spawns = new List<GameObject>();
    float timer;
    float nextSpawn;

    PhaseManager pManager;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        GameObject[] sceneSpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
        foreach(GameObject g in sceneSpawns)
        {
            Spawns.Add(g);
        }
        timer = 0f;
        nextSpawn = 0f;

        pManager = this.GetComponent<PhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // While there are still enemies to place on the screen, spawn in an enemy every 1 - 3 seconds
        if (pManager.GetCurrentState() == PhaseState.Attack && numEnemies > 0)
        {
            timer += Time.deltaTime;
            if (timer >= nextSpawn)
            {
                SpawnEnemy();
                timer = 0f;
                --numEnemies;
                nextSpawn = Random.Range(1f, 3f);
            }
        }
    }

    void SpawnEnemy()
    {
        enemies.Add(Instantiate(enemyPrefab, Spawns[Random.Range(0, Spawns.Count)].transform.position, Quaternion.identity));
        enemies[enemies.Count - 1].GetComponent<EnemyScript>().eManager = this;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if(enemies.Contains(enemy))
            enemies.Remove(enemy);
    }
}
