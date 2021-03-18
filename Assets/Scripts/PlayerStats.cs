using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider shieldsBar;
    public Slider healthBar;

    public float health;
    public float shields;
    public float maxHealth=50;
    public float maxShields=75;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shields = maxShields;
        

    }

    // Update is called once per frame
    void Update()
    {
        shieldsBar.value = shields;
        healthBar.value = health;

        
    }
}
