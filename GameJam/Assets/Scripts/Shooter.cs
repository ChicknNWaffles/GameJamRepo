using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootTransform;
    public Vector3 shootDir = new Vector2(0.819152f, 0.573576f);
    public float shootDelay = .85f;
    public float projectileDelay = .25f;
    public float shootForce = 10f;

    Vector3 shootTemp;
    Animator animator;
    float nextShoot;
    float projectileTime;
    bool doShoot;

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
        projectileTime = Time.time + projectileDelay;
        doShoot = true;

        if (animator != null)
        {
            animator.Play("PlayerAttack");
        }
    }

    void FireProjectile()
    {
        doShoot = false;

        shootTemp = transform.localScale.x < 0f ? new Vector3(-shootDir.x, shootDir.y) : shootDir;

        if (GetComponent<Rigidbody2D>())
        {
            shootTemp += (Vector3)GetComponent<Rigidbody2D>().velocity * .05f;
        }

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootTransform.position;
        projectile.GetComponent<Rigidbody2D>().AddForce(shootTemp * shootForce, ForceMode2D.Impulse);
        //projectile.GetComponent<Rigidbody2D>().AddTorque(shootForce * .1f, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        if (doShoot && Time.time > projectileTime)
        {
            FireProjectile();
        }
    }
}
