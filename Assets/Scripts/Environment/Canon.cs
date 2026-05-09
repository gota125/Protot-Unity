using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject Projectile;
   public float fireRate;
   private float nextFire = 0f ;
   public Vector2 direction;
   public Transform FirePoint;
   public GameObject self;
   public float SpeedProjectile;
  
    
    
    void Update()
    {
        if (Time.time > nextFire)
        {
            FireProjectile();
            nextFire = Time.time + 1f/fireRate;
        }
        
    }
    

    void FireProjectile()
    {
        GameObject spawnedMissile = Instantiate(Projectile, FirePoint.position, Quaternion.identity);

        Projectile projectile = spawnedMissile.GetComponent<Projectile>();
        projectile.speed = -FirePoint.up * SpeedProjectile;
    }
}

