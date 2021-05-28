using UnityEngine;

public class Damage : MonoBehaviour
{
    public float health = 100f;
    public PlayerStats player;
    public GameObject manager;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            player.enemiesKilled++;
            Die();
        }

    }

    void Die()
    {
        manager.GetComponent<EnemyManager>().enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
