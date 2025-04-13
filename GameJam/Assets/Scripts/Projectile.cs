using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public float damage;
    public bool damagePlayer = false;
    public UnityEvent onHitEvent;
    public bool killOnHit;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        onHitEvent.Invoke();
        if (killOnHit) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damageObject = collision.gameObject.GetComponentInChildren<IDamageable>() != null ? collision.gameObject.GetComponentInChildren<IDamageable>() : collision.gameObject.GetComponentInParent<IDamageable>();

        if (damageObject != null)
        {
            damageObject.TakeDamage(damage);
        }
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageObject = collision.gameObject.GetComponentInChildren<IDamageable>() != null ? collision.gameObject.GetComponentInChildren<IDamageable>() : collision.gameObject.GetComponentInParent<IDamageable>();

        if (damageObject != null)
        {
            damageObject.TakeDamage(damage);
            Die();
        }
    }
}
