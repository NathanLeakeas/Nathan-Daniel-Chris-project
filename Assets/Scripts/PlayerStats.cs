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
    public GameObject enemyIndicator;

    public float health;
    public float shields;
    public float maxHealth=50;
    public float maxShields=75;
    public float maxMapDistance=25;

    public float blipSize=50;

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
        //minimap drawing
        GameObject[] blips = GameObject.FindGameObjectsWithTag("EnemyIndicator");
        foreach(GameObject blip in blips)
        {
            Destroy(blip);
        }


        foreach (GameObject enemy in enemyManager.enemies)
        {
            Vector3 enemyPos = this.transform.InverseTransformPoint(enemy.transform.position);
            if (maxMapDistance >= (
                Mathf.Sqrt(Mathf.Pow(enemyPos.x, 2) + Mathf.Pow(enemyPos.z, 2))
               ))
            {
                //draw enemy on minimap here
                enemyPos *= (50/maxMapDistance/*Minmap Height*/);//enemyPos now contains coordinates for the blip on the minimap
                GameObject indicator = (GameObject)Instantiate(enemyIndicator);
                indicator.transform.SetParent(minimap.transform);

                indicator.transform.localPosition = new Vector3(enemyPos.x, enemyPos.z, 0);

            }
        }

        
    }

    public void takeDamage(int damage)
    {
        if (shields > 0)
        {
            shields -= damage;
            if (shields < 0)
            {
                health += shields;
                shields = 0;
            }
        }
        else
        { 
            health-=damage;
        }
    }

}
