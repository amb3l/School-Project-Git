using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpitterController : EnemyController
{
    [Header("Shooting")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float shootDelay;
                     private float lastShootTime = 0;

    [Header("Gravity")]
    [SerializeField] private float gravityAngle;
    [SerializeField] private float gravityForce;
    
    protected override void Start()
    {
        base.Start();
        enemyRB.gravityScale = 0;
        transform.Rotate(0, 0, gravityAngle);
    }
    protected override void Update()
    {
        base.Update();
        if (Time.time - lastShootTime > shootDelay)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }
    protected void Shoot()
    {
        GameObject prj = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        prj.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
        Destroy(prj, 3);
    }

    protected override void Move()
    {
        enemyRB.velocity = transform.up * (-gravityForce) + transform.right * speed;

    }
}
