using System;
using Mono.Cecil;
using UnityEngine;

public class MiniBoss2_Script : MonoBehaviour
{


    [Header("Paramètres de Mouvement")]
    public Transform player;
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float stoppingDistance = 2f;

    [Header("Paramètres d'Attaque (Rotation)")]
    public bool isSpinning = false;
    public float spinSpeed = 500f;
    
    public float bossMaxHealth = 100f;
    public float currentHealth= 100f;

    private void Update()
    {
        if (player == null) return;

        // 1. Calcul de la distance
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 2. Gestion de la Rotation (Look At Player)
        if (!isSpinning)
        {
            RotateTowardsPlayer();
        }
        else
        {
            // Mode Toupie : tourne frénétiquement sur lui-même
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        }

        // 3. Gestion du Déplacement
        if (distanceToPlayer > stoppingDistance)
        {
            MoveTowardsPlayer();
        }
        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
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

    void RotateTowardsPlayer()
    {
        // Direction vers le joueur
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // On soustrait 90 si ton sprite pointe vers le haut par défaut
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f);
        
        // Rotation fluide
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveTowardsPlayer()
    {
        // Déplacement vers l'avant (direction haut du sprite)
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
    void TakeDamage()
    {
        currentHealth -= 10f;
        Debug.Log("Le Boss a pris des dégâts ! Vie restante : " + currentHealth);
    }
    
    
}



