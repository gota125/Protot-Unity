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
   public float detectionRange;
   public float forgetPlayer;
   public float distanceToPlayer;
   public GameObject player;
   public bool canShoot = false;
  
    
    
    void Update()
    {
        UpdateDistanceToPlayer();
        UpdateShootingState();
        if (Time.time > nextFire)
        {
            if (canShoot)
            {


                FireProjectile();
                nextFire = Time.time + 1f / fireRate;
            }
        }
        
    }
    

    void FireProjectile()
    {
        GameObject spawnedMissile = Instantiate(Projectile, FirePoint.position, Quaternion.identity);

        Projectile projectile = spawnedMissile.GetComponent<Projectile>();
        projectile.speed = -FirePoint.up * SpeedProjectile;
    }
    private void UpdateDistanceToPlayer()
    {
        distanceToPlayer = Vector2.Distance(self.transform.position, player.transform.position);
        
    }

    private void UpdateShootingState()
    {
        if (distanceToPlayer < detectionRange)
        {
            canShoot = true;
            
        }

        if (distanceToPlayer > forgetPlayer)
        {
            canShoot = false;
            
        }

        
    }
}

