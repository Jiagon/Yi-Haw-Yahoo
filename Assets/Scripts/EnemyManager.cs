using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public uint numEnemies = 0;

    List<GameObject> Spawns = new List<GameObject>();
    float timer;
    float nextSpawn;

    public PhaseManager phaseManager;
    public Transform enemyPrefab;
    public GameObject table;
    PlaceableManager placeManager;
    int enemiesKilled = 0;
    public int totalEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalEnemies = (int)numEnemies;
        enemies = new List<GameObject>();
        GameObject[] sceneSpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
        foreach(GameObject g in sceneSpawns)
        {
            Spawns.Add(g);
        }
        phaseManager = this.GetComponent<PhaseManager>();
        placeManager = this.GetComponent<PlaceableManager>();

        ResetEnemies(numEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count == 0 && numEnemies == 0) {
            phaseManager.EnterGameOver();
            GameObject.Find("Outcome").GetComponent<Text>().text = "VICTORY";
        }
        // While there are still enemies to place on the screen, spawn in an enemy every 1 - 3 seconds
        if (phaseManager.GetCurrentState() == PhaseState.Attack && numEnemies > 0)
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

    public void KillEnemy() {
        enemiesKilled++;
    }

    void SpawnEnemy()
    {
        Transform newEnemy = Instantiate(enemyPrefab,Spawns[Random.Range(0, Spawns.Count)].transform.position,Quaternion.identity);
        newEnemy.parent = table.transform;
        enemies.Add(newEnemy.gameObject);
        enemies[enemies.Count - 1].GetComponent<EnemyScript>().eManager = this;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if(enemies.Contains(enemy))
            enemies.Remove(enemy);
    }

    public void RemovePlaceable(GameObject placeable)
    {
        placeManager.RemovePlaceable(placeable);
    }

    public List<GameObject> GetPlaceables()
    {
        return placeManager.placeables;
    }

    public void ResetEnemies(uint enemyCount)
    {
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
        timer = 0f;
        nextSpawn = 0f;
        numEnemies = enemyCount;
    }
}
