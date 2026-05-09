using System;
using Mono.Cecil;
using UnityEngine;

public class MiniBoss1_Script : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public GameObject player;
    [SerializeField] private GameObject self;
    public Transform canon;
    
    public Transform spawnPointRotatif;
    public Transform spawnPointRotatif2;
    public Transform canonRotatif;
    
    
    public int projectileSpeed = 5;
    public int projectileSpeed2 = 10;
    private float selfToPlayerDist;
    private float _timer;
    private float _timer2;
    [SerializeField] private float StopNearPlayer;
    public float fireRate = 1f;
    public float fireRateRotatif = 0.5f;
    [SerializeField] private float isNearPlayer;
    private bool canshoot;
    public static bool  startshoot = false;
   [SerializeField] private bool acivatedVariant = false;

   public float ennemyHealth;
   public float ennemyMaxHealth = 3;


   private void Start()
   {
       ennemyHealth = ennemyMaxHealth;
   }

   void Update()
   {
       if (player != null)
       {
           selfToPlayerDist = Vector2.Distance(self.transform.position, player.transform.position);

            /*
           if (acivatedVariant)
           {
               VarCanShoot();
               if (canshoot)
               {
                   AimProjectile();
                   if (Time.time > _timer)
                   {
                       FireProjectile();

                       _timer = Time.time + fireRate;
                   }
               }
           }
           else
           {
               CanShoot();
               if (canshoot)
               {
                   AimProjectile();
                   if (Time.time > _timer)
                   {
                       FireProjectile();

                       _timer = Time.time + fireRate;
                   }
               }
           }
           */
           if (acivatedVariant)
           {
               VarCanShoot();
               if (canshoot)
               {
                   AimProjectile();
                   AimRotatif();
                   if (Time.time > _timer)
                   {
                       FireProjectileRotatif();
                       FireProjectileRotatif2();

                       _timer = Time.time + fireRateRotatif;
                   }
                   if (Time.time > _timer2)
                   {
                       FireProjectile();

                       _timer2 = Time.time + fireRate;
                   }
               }
           }
           else
           {
               CanShoot();
               if (canshoot)
               {
                   AimProjectile();
                   AimRotatif();
                   if (Time.time > _timer)
                   {
                       FireProjectileRotatif();
                       FireProjectileRotatif2();

                       _timer = Time.time + fireRateRotatif;
                   }
                   if (Time.time > _timer2)
                   {
                       FireProjectile();

                       _timer2 = Time.time + fireRate;
                   }
               }
           }

       }
       
       Die();
   }

   void FireProjectile()
   {
       GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

       Projectile projectile = spawnedMissile.GetComponent<Projectile>();
       projectile.speed = spawnPoint.up * projectileSpeed;

       projectile.owner = gameObject;
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
    
    public void AimRotatif()
       {
           canonRotatif.transform.RotateAround(self.transform.position, Vector3.back, 40 * Time.deltaTime);
       }
    void FireProjectileRotatif()
    {
        GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPointRotatif.position, Quaternion.identity);

        Projectile projectile = spawnedMissile.GetComponent<Projectile>();
        projectile.speed = spawnPointRotatif.up * projectileSpeed2;

        projectile.owner = gameObject;
    }
    
    void FireProjectileRotatif2()
    {
        GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPointRotatif2.position, Quaternion.identity);

        Projectile projectile = spawnedMissile.GetComponent<Projectile>();
        projectile.speed = spawnPointRotatif2.up * projectileSpeed2;

        projectile.owner = gameObject;
    }
    
    

    void Die()
    {
        if (ennemyHealth <= 0)
        {
             GameManager.Instance.AddEnemyKill();
             Destroy(gameObject);
        }
    }

    void EnnemyTakeDamage()
    {
        ennemyHealth -= 1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Projectile proj = other.gameObject.GetComponent<Projectile>();

        if (proj != null && proj.owner != gameObject)
        {
            EnnemyTakeDamage();
        }
    }

    private void CanShoot()
    {
        if (selfToPlayerDist < isNearPlayer)
        {
            startshoot = true;
        }
        
        if (startshoot)
        {
            canshoot = true;
        }
        else
        {
            canshoot = false;
        }
    }
    private void VarCanShoot()
    {
        if (selfToPlayerDist < isNearPlayer)
        {
            canshoot = true;
        }
        else
        {
            canshoot = false;
        }
    }
    
    
    
    
}
