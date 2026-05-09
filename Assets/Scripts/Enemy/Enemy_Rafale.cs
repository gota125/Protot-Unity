using System.Collections;
using UnityEngine;

public class EnemyRafale : MonoBehaviour
{
    [Header("Settings")]
    public float projectileSpeed = 10f;
    public float fireRate = 1.5f;
    public float isNearPlayer = 10f;
    public bool isVariant = false;
    public bool useBurstMode = false;

    [Header("References")]
    public GameObject projectilePrefab;
    public GameObject player;
    public Transform canon;
    // Glisse tous tes points de tir dans ce tableau dans l'inspecteur
    public Transform[] spawnPoints; 

    private float _timer;
    private bool _hasSpottedPlayer = false;

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.transform.position);
        
        if (CheckIfCanShoot(dist))
        {
            AimAtPlayer();

            if (Time.time > _timer)
            {
                if (useBurstMode) StartCoroutine(BurstRoutine());
                else FireFromSpawnPoint(0); // Tir simple depuis le premier point

                _timer = Time.time + fireRate;
            }
        }
    }

    // Gère la logique de détection
    private bool CheckIfCanShoot(float currentDist)
    {
        if (isVariant) return currentDist < isNearPlayer;

        // Logique originale : une fois qu'il a vu le joueur, il n'arrête plus de tirer
        if (currentDist < isNearPlayer) _hasSpottedPlayer = true;
        return _hasSpottedPlayer;
    }

    // Méthode de tir unique et universelle
    private void FireFromSpawnPoint(int index)
    {
        if (index >= spawnPoints.Length || spawnPoints[index] == null) return;

        Transform sp = spawnPoints[index];
        GameObject bullet = Instantiate(projectilePrefab, sp.position, sp.rotation);
        
        Projectile proj = bullet.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.speed = sp.up * projectileSpeed;
            proj.owner = gameObject;
        }
    }

    private void AimAtPlayer()
    {
        Vector2 direction = player.transform.position - canon.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // On soustrait 90 si ton sprite de canon pointe vers le haut par défaut
        canon.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    IEnumerator BurstRoutine()
    {
        // Tire depuis les points 0, 1 et parfois 2
        FireFromSpawnPoint(0);
        yield return new WaitForSeconds(0.2f);
        FireFromSpawnPoint(1);
        
        if (Random.value < 0.5f)
        {
            yield return new WaitForSeconds(0.2f);
            FireFromSpawnPoint(2);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Projectile proj = other.gameObject.GetComponent<Projectile>();
        if (proj != null && proj.owner != gameObject)
        {
            GameManager.Instance.AddEnemyKill();
            Destroy(gameObject);
        }
    }
}