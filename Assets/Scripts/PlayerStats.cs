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
            if (maxMapDistance >= (Mathf.Sqrt(Mathf.Pow(enemyPos.x, 2)) + Mathf.Sqrt(Mathf.Pow(enemyPos.z, 2))))
            {
                //draw enemy on minimap here
                enemyPos *= (50/maxMapDistance/*Minmap Height*/);//enemyPos now contains coordinates for the blip on the minimap
                Debug.Log(enemyPos);
                GameObject indicator = (GameObject)Instantiate(enemyIndicator);
                indicator.transform.SetParent(minimap.transform);
                RectTransform rt = indicator.GetComponent<RectTransform>();

                /*rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, enemyPos.x, blipHeight);
                rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, enemyPos.z, blipWidth);*/
                indicator.transform.localPosition = new Vector3(enemyPos.x, enemyPos.z, 0);
                //indicator.transform.localScale = new Vector3(blipSize,blipSize,blipSize);

            }
        }

        
    }
}
