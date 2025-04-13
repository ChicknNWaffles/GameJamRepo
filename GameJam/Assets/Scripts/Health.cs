using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f;
    public UnityEvent onDamageEvent;
    public UnityEvent onDieEvent;

    public float HealthAmount { get; private set; }

    public void TakeDamage(float damage)
    {
        HealthAmount = HealthAmount - damage;

        onDamageEvent.Invoke();

        if (HealthAmount <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        print("Agh Im dead");
        onDieEvent.Invoke();
    }

    public void ResetHealth()
    {
        HealthAmount = startingHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthAmount = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
