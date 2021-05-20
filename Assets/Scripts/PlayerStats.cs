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

    public bool dead;
    public float health;
    public float shields;
    public float maxHealth=50;
    public float maxShields=75;
    public float maxMapDistance=25;

    public float timeInGame;

    public Text time;
    public Text killcount;
    public Text accuracy;
    public int totalShots;
    public int shotsHit;
    public float accuracyRate;

    public int enemiesKilled;

    public float blipSize=50;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shields = maxShields;
        enemiesKilled = 0;
        enemyManager = manager.GetComponent<EnemyManager>();
        timeInGame = 0;
        totalShots = 0;
        shotsHit = 0;
        accuracyRate = 0f;



    }

    // Update is called once per frame
    void Update()
    {
        timeInGame += Time.deltaTime;

        killcount.text = enemiesKilled.ToString();

        int minutesInGame = ((int)timeInGame)/60;
        int secondsInGame = ((int)timeInGame) - minutesInGame * 60;
        string zero = "";
        if (secondsInGame < 10)
        {
            zero = "0";
        }
        time.text = "" + minutesInGame.ToString() + ":" +zero + secondsInGame.ToString();
        if (totalShots > 0)
        {
            accuracyRate = (shotsHit*100 / totalShots );
            Debug.Log(accuracyRate);
            
        }
        accuracy.text = "" + (int)accuracyRate+"%";


        

        foreach (Transform child in shieldsBar.transform.parent)
        {
            GameObject element = child.gameObject;
            if (element.tag == "Scoreboard")
            {
                element.SetActive(Input.GetKey(KeyCode.Tab));
            }
        }

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
        if (health <= 0)
        {
            dead = true;
            //Time.timeScale = 0f;
        }
    }

}
