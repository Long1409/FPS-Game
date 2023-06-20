using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;

    [SerializeField] protected bool isDead;

    private void Start()
    {
        SetHealthTo(maxHealth);
        isDead = false;
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            health = 0;
            isDead = true;
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Dead()
    {
        isDead = true;
    }

    private void SetHealthTo(int healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        int healthAfterDamge = health - damage;
        SetHealthTo(healthAfterDamge);
    }

    public void Heal(int heal)
    {
        int healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal); 
    }
}
