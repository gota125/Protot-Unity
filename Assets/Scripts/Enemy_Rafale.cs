using System;
using Mono.Cecil;
using UnityEngine;
using System.Collections;
using Random = System.Random;


public class EnemyRafale : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public GameObject player;
    [SerializeField] private GameObject self;
    public Transform canon;
    
    
    public int projectileSpeed = 5;
    private float selfToPlayerDist;
    private float _timer;
    [SerializeField] private float StopNearPlayer;
    public float fireRate = 0.5f;
    [SerializeField] private float isNearPlayer;
    private bool canshoot;
    private bool startshoot = false;
   [SerializeField] private bool acivatedVariant = false;
    public bool variante_3_projectile = false;


   void Update()
   {
       if (player != null)
       {
           selfToPlayerDist = Vector2.Distance(self.transform.position, player.transform.position);


           if (acivatedVariant)
           {
               VarCanShoot();
               if (canshoot)
               {
                   AimProjectile();
                   if (Time.time > _timer)
                   {
                       if (variante_3_projectile == true)
                       {
                          StartBurst();
                       }
                       else
                       {
                           FireShotgun1();
                       }

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
                       if (variante_3_projectile == true)
                       {
                           StartBurst();
                       }
                       else
                       {
                           FireShotgun1();
                       }

                       _timer = Time.time + fireRate;
                   }
               }
           }

       }
   }

   void FireProjectile()
   {
       GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

       Projectile projectile = spawnedMissile.GetComponent<Projectile>();
       projectile.speed = spawnPoint.up * projectileSpeed;

       projectile.owner = gameObject;
   }
   
   
   void FireShotgun1()
   {
       GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

       Projectile projectile = spawnedMissile.GetComponent<Projectile>();
       
       projectile.speed = spawnPoint.up *  projectileSpeed;
       

       projectile.owner = gameObject;
   }
   
   void FireShotgun2()
   {
       GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint1.position, Quaternion.identity);

       Projectile projectile = spawnedMissile.GetComponent<Projectile>();
       
       projectile.speed = spawnPoint1.up *  projectileSpeed;
       

       projectile.owner = gameObject;
   }
   void FireShotgun3()
   {
       GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint2.position, Quaternion.identity);

       Projectile projectile = spawnedMissile.GetComponent<Projectile>();
       
       projectile.speed = spawnPoint2.up *  projectileSpeed;
       

       projectile.owner = gameObject;
   }
   void FireShotgun4()
   {
       GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint3.position, Quaternion.identity);

       Projectile projectile = spawnedMissile.GetComponent<Projectile>();
       
       projectile.speed = spawnPoint3.up *  projectileSpeed;
       

       projectile.owner = gameObject;
   }
   void FireShotgun5()
   {
       GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint4.position, Quaternion.identity);

       Projectile projectile = spawnedMissile.GetComponent<Projectile>();
       
       projectile.speed = spawnPoint4.up *  projectileSpeed;
       

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
    

    void Die()
    {
        GameManager.Instance.AddEnemyKill();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Projectile proj = other.gameObject.GetComponent<Projectile>();

        if (proj != null && proj.owner != gameObject)
        {
            Die();
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
    
    public void StartBurst()
    {
        StartCoroutine(Burst());
    }

    IEnumerator Burst()
    {
        FireShotgun1();
        yield return new WaitForSeconds(0.5f);

        FireShotgun2();
        yield return new WaitForSeconds(0.5f);
        
        if (UnityEngine.Random.value < 0.5f)
        {
            FireShotgun3();
        }
        
    }
    
}
