using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootTransform;
    public float shootDelay = .7f;

    float nextShoot;

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time < nextShoot) return;
        nextShoot = Time.time + shootDelay;

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootTransform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
}
