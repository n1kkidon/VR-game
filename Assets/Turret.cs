using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    void Update()
    {
        FindNearestEnemy();

        if (fireCountdown <= 0f && target != null)
        {
            // Shoot the projectile
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        //GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        //Projectile projectile = projectileGO.GetComponent<Projectile>();

        //if (projectile != null)
        //{
        //    projectile.Seek(target);
        //}
    }
}
