using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootTransform;
    public Vector3 shootDir = new Vector2(0.819152f, 0.573576f);
    public float shootDelay = .7f;
    public float shootForce = 10f;

    Vector3 shootTemp;

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

        shootTemp = transform.localScale.x < 0f ? new Vector3(-shootDir.x, shootDir.y) : shootDir;

        nextShoot = Time.time + shootDelay;

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootTransform.position;
        projectile.GetComponent<Rigidbody2D>().AddForce(shootTemp * shootForce, ForceMode2D.Impulse);
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
