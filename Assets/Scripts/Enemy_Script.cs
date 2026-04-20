using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    [SerializeField]public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float projectileSpeed;
    
    void Update()
    {
        
    }


    public void FireProjectile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint.position,Quaternion.identity);

            Projectile projectile = spawnedMissile.GetComponent<Projectile>();
            projectile.speed = spawnPoint.up * projectileSpeed;
        }
    }
}
