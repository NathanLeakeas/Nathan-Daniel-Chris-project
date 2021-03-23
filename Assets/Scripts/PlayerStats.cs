using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider shieldsBar;
    public Slider healthBar;
    public GameObject manager;
    public EnemyManager enemyManager;
    public GameObject minimap;

    public float health;
    public float shields;
    public float maxHealth=50;
    public float maxShields=75;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shields = maxShields;
        enemyManager = manager.GetComponent<EnemyManager>();



    }

    // Update is called once per frame
    void Update()
    {
        shieldsBar.value = shields;
        healthBar.value = health;
        foreach (GameObject enemy in enemyManager.enemies)
        {
            Vector3 enemyPos = this.transform.InverseTransformPoint(enemy.transform.position);
            if (enemyPos.x < 10 && enemyPos.z < 10)
            { 
                //draw enemy on minimap here

            }
        }

        
    }
}
