using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public enum ShootType
    {
        Single,
        Shotgun,
        Burst,
        ProjectileVariant,
        Sniper
    }
    [Header("References")]
    public GameObject projectilePrefab;
    public GameObject projectileImparablePrefab;
    public GameObject lifePrefab;
    public GameObject player;
    public Transform spawnPoint;
    public Transform canon;
    

    [SerializeField] private GameObject self;

    [Header("Shooting Settings")]
    public int projectileSpeed = 5;
    public float fireRate = 0.5f;
    [SerializeField] private float detectionRange;
    public ShootType shootType = ShootType.Single;

    private float nextFireTime;
    private float distanceToPlayer;
    private bool canShoot = false;
    private bool hasStartedShooting = false;
    public bool HasDetectedPlayer => hasStartedShooting;
    [SerializeField] private float forgetPlayer; 

    [Header("Shotgun Settings")]
    public int shotgunProjectileCount = 3;
    public float shotgunSpreadAngle = 20f;
    
    
    [Header("Burst Settings")]
    public int burstProjectileCount = 3;
    public float burstDelay = 0.1f;
    public float burstAngleVariation = 10f;

    private bool isBursting = false;
    
    [Header("Sniper Settings")]
    public LineRenderer laser;
    public float aimDuration = 2f;
    public float blinkSpeed = 0.15f;
    public float sniperProjectileSpeed = 20f;
    public float sniperCooldown = 1f;

    private bool isSniping = false;

    [Header("Health")]
    public float enemyMaxHealth = 3;
    public float enemyHealth;

    [Header("Loot")]
    public float lifeSpawnChancePercent = 50f;

    private void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    private void Update()
    {
        if (player != null)
        {
            UpdateDistanceToPlayer();
            UpdateShootingState();

            if (canShoot)
            {
                AimProjectile();

                if (Time.time > nextFireTime)
                {
                    FireProjectile();

                    nextFireTime = Time.time + fireRate;
                }
            }
        }

        CheckDeath();
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
            hasStartedShooting = true;
        }

        if (distanceToPlayer > forgetPlayer)
        {
            canShoot = false;
            hasStartedShooting =  false;
        }

        
    }

    private void SpawnProjectile(float angleOffset)
    {
        Quaternion rotation = spawnPoint.rotation * Quaternion.Euler(0, 0, angleOffset);

        GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint.position, rotation);

        Projectile projectile = spawnedMissile.GetComponent<Projectile>();

        projectile.speed = rotation * Vector2.up * projectileSpeed;
        projectile.owner = gameObject;
    }
    
    private void SpawnProjectileVariant(float angleOffset)
    {
        Quaternion rotation = spawnPoint.rotation * Quaternion.Euler(0, 0, angleOffset);

        GameObject spawnedMissile = Instantiate(projectileImparablePrefab, spawnPoint.position, rotation);

        Projectile projectile = spawnedMissile.GetComponent<Projectile>();

        projectile.speed = rotation * Vector2.up * projectileSpeed;
        projectile.owner = gameObject;
    }
    
    private void FireProjectile()
    {
        switch (shootType)
        {
            case ShootType.Single:
                SpawnProjectile(0f);
                break;

            case ShootType.Shotgun:
                FireShotgun();
                break;
            case ShootType.Burst:
                if (!isBursting)
                    StartCoroutine(BurstFire());
                break;
            case ShootType.ProjectileVariant:
                SpawnProjectileVariant(0f);
                break;
            case ShootType.Sniper:
                if (!isSniping)
                    StartCoroutine(SniperShot());
                break;
        }
    }

    private void AimProjectile()
    {
        Vector3 directionToPlayer = player.transform.position - canon.position;

        float angle = Vector3.Angle(directionToPlayer, Vector3.up);
        Vector3 cross = Vector3.Cross(directionToPlayer, Vector3.up);

        canon.eulerAngles = cross.z > 0
            ? new Vector3(0, 0, -angle)
            : new Vector3(0, 0, angle);
    }

    private void TakeDamage()
    {
        enemyHealth -= 1;
    }

    private void CheckDeath()
    {
        if (enemyHealth <= 0)
        {
            GameManager.Instance.AddEnemyKill();
            SpawnLifeOnDeath();
            Destroy(gameObject);
        }
    }

    private void SpawnLifeOnDeath()
    {
        float randomNumber = Random.Range(0, 100);

        if (randomNumber <= lifeSpawnChancePercent)
        {
            Instantiate(
                lifePrefab,
                self.transform.position,
                Quaternion.Euler(0, 0, -45)
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Projectile proj = other.gameObject.GetComponent<Projectile>();

        if (proj != null && proj.owner != gameObject)
        {
            TakeDamage();
        }
    }
    
    private void FireShotgun()
    {
        if (shotgunProjectileCount <= 1)
        {
            SpawnProjectile(0f);
            return;
        }

        float angleStep = shotgunSpreadAngle / (shotgunProjectileCount - 1);
        float startAngle = -shotgunSpreadAngle / 2f;

        for (int i = 0; i < shotgunProjectileCount; i++)
        {
            float angle = startAngle + (angleStep * i);
            SpawnProjectile(angle);
        }
    }
    private IEnumerator BurstFire()
    {
        isBursting = true;

        for (int i = 0; i < burstProjectileCount; i++)
        {
            float randomAngle = Random.Range(
                -burstAngleVariation,
                burstAngleVariation
            );

            SpawnProjectile(randomAngle);

            yield return new WaitForSeconds(burstDelay);
        }

        isBursting = false;
    }
    
    private IEnumerator SniperShot()
    {
        isSniping = true;
        laser.enabled = true;

        float timer = 0f;
        bool isRed = true;

        while (timer < aimDuration)
        {
            UpdateLaserAim();

            laser.startColor = isRed ? Color.red : Color.indianRed;
            laser.endColor = isRed ? Color.red : Color.indianRed;

            isRed = !isRed;

            yield return new WaitForSeconds(blinkSpeed);

            timer += blinkSpeed;
        }

        FireSniperProjectile();

        laser.enabled = false;

        yield return new WaitForSeconds(sniperCooldown);

        isSniping = false;
    }
    
    private void UpdateLaserAim()
    {
        if (player == null) return;

        Vector3 direction = (player.transform.position - spawnPoint.position).normalized;

        laser.SetPosition(0, spawnPoint.position);
        laser.SetPosition(1, spawnPoint.position + direction * 40f);
    }
    
    private void FireSniperProjectile()
    {
        Vector3 direction = (player.transform.position - spawnPoint.position).normalized;

        GameObject projectileObj = Instantiate(
            projectilePrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        Projectile projectile = projectileObj.GetComponent<Projectile>();

        projectile.speed = direction * sniperProjectileSpeed;
        projectile.owner = gameObject;
    }
}