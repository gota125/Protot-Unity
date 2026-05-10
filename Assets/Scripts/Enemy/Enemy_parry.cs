using UnityEngine;

public class EnemyParry : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 100)] 
    public float parryChance = 50f; 
    public float reflectSpeedMultiplier = 1.2f;
    public Color projectileColor = Color.green;
    [Header("References")]
    public Collider2D enemyCollider; // Ton "himSelf"

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Projectile projectile = other.GetComponent<Projectile>();

            // CONDITION CRUCIALE : 
            // L'ennemi ne doit parer QUE si le projectile n'est pas le sien (donc celui du joueur)
            if (projectile != null && projectile.owner != gameObject)
            {
                // Lancer le jet de dés
                if (Random.Range(0f, 100f) <= parryChance)
                {
                    ParryProjectile(projectile);
                }
                else
                {
                    Debug.Log("L'ennemi a raté sa parade !");
                }
            }
        }
    }

// J'ai légèrement modifié les paramètres pour passer directement le script Projectile
    private void ParryProjectile(Projectile projectile)
    {
        Debug.Log("L'ennemi a réussi sa parade !");

        // Inverser la vitesse
        projectile.speed = -projectile.speed * reflectSpeedMultiplier;

        // Redevenir le propriétaire (devient rouge)
        projectile.ChangeOwnership(gameObject, projectileColor); 
    }
    }
