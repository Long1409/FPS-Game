using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float health;

    public void takeDamage(int explosionDamage)
    {
        health -= explosionDamage;

        if (health <= 0) destroyEnemy();
    }

    private void destroyEnemy()
    {
        Destroy(gameObject);
    }
}
