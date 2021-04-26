using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> spawns;
    public List<GameObject> enemies;
    public GameObject enemyPrefab;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //initial spawns
        foreach(GameObject spawn in spawns)
        {
            SpawnEnemy(spawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(GameObject spawn)
    {
        Vector3 spawnPos = spawn.transform.position;
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(spawnPos.x, spawnPos.y + 1.0f, spawnPos.z), Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().target = player;
        enemies.Add(enemy);


    }
}
