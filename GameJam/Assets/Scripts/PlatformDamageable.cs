using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDamageable : PlatformSlide, IDamageable
{
    public float startingHealth;

    public float Health { get; protected set; }

    public void TakeDamage(float damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // GOTO implement proper death mechanic
        Destroy(gameObject);
    }

}
