using System;
using Mono.Cecil;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public GameObject player;
    public Transform canon;
    
    public int projectileSpeed = 5;
    private float _timer;
    public float fireRate = 0.5f;
    
    
    void Update()
    {
        AimProjectile();
        if (Time.time > _timer)
        {
            FireProjectile();
             
             _timer = Time.time + fireRate;
        }
        
    }

    void FireProjectile()
    {
        GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint.position,Quaternion.identity);
        Projectile projectile = spawnedMissile.GetComponent<Projectile>();
        projectile.speed = spawnPoint.up * projectileSpeed;
    }
    


    public void AimProjectile()
    {
        
        Vector3 canonToPlayer = player.transform.position - canon.position;
        Vector3 top = Vector3.up;
        
        float angle = Vector3.Angle(canonToPlayer, top);
        
        Vector3 cross = Vector3.Cross(canonToPlayer, top);
        
        if(cross.z > 0)
            canon.eulerAngles = new Vector3(0, 0, -angle);
        else
            canon.eulerAngles = new Vector3(0, 0, angle);
        
    }
    
}
