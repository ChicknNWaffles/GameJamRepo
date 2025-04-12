using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    public float damageDelay = .25f;
    public float damagePerTick = 5f;

    float nextDamageTick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStartDamage(GameObject damaged)
    {
        print($"Applying {damagePerTick} damage");
        nextDamageTick = Time.time + damageDelay;
    }

    void OnDamageTick(GameObject damaged)
    {
        if (Time.time > nextDamageTick)
        {
            print($"Applying {damagePerTick} damage");
            nextDamageTick = Time.time + damageDelay;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnStartDamage(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnDamageTick(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnStartDamage(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnDamageTick(collision.gameObject);
        }
    }
}
