using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    Vector3 respawnPoint;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        
        if (health != null)
        {
            health.ResetHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            respawnPoint = collision.transform.position;
        }
    }
}
