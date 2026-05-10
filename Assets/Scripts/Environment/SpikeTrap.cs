using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    [Header("Réglages du Timing")] public float activeDuration = 2f; // Temps où les pics sont sortis
    public float idleDuration = 2f; // Temps où les pics sont cachés
    public float warningDuration = 0.5f; // Petit délai visuel avant de piquer

    private SpriteRenderer spriteRenderer;
    private Collider2D spikeCollider;
    private bool isActive = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spikeCollider = GetComponent<Collider2D>();

        // Lance la boucle du piège
        StartCoroutine(TrapCycle());
    }

    IEnumerator TrapCycle()
    {
        while (true) // Boucle infinie
        {
            // ÉTAPE 1 : IDLE (Caché)
            isActive = false;
            spikeCollider.enabled = false;
            spriteRenderer.color = new Color(1, 1, 1, 0.2f); // Optionnel : translucide
            yield return new WaitForSeconds(idleDuration);

            // ÉTAPE 2 : WARNING (Clignotement ou couleur différente)
            spriteRenderer.color = Color.orange;
            yield return new WaitForSeconds(warningDuration);

            // ÉTAPE 3 : ACTIVE (Dangereux)
            isActive = true;
            spikeCollider.enabled = true;
            spriteRenderer.color = Color.red; // Les pics sont sortis !
            yield return new WaitForSeconds(activeDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            Debug.Log("Le joueur a touché les pointes !");
            GameManager.Instance.PlayerTakeDamage();
        }

        if (isActive && other.CompareTag("Ennemy"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                
                enemy.ennemyHealth -= 1;
                
                Debug.Log("Un ennemi a été empalé !");

            }
        }
    }
}