using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("References")]
    public GameObject projectilePrefab;
    public GameObject lifePrefab;
    public GameObject player;
    public Transform spawnPoint;
    public Transform canon;
    

    [SerializeField] private GameObject self;

    [Header("Shooting Settings")]
    public int projectileSpeed = 5;
    public float fireRate = 0.5f;
    [SerializeField] private float detectionRange;

    private float nextFireTime;
    private float distanceToPlayer;
    private bool canShoot;
    private bool hasStartedShooting = false;
    public bool HasDetectedPlayer => hasStartedShooting;

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
            hasStartedShooting = true;

        canShoot = hasStartedShooting;
    }

    private void FireProjectile()
    {
        GameObject spawnedMissile = Instantiate(
            projectilePrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        Projectile projectile = spawnedMissile.GetComponent<Projectile>();
        projectile.speed = spawnPoint.up * projectileSpeed;
        projectile.owner = gameObject;
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
}