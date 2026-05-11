using UnityEngine;

public class Checkpoint_Script: MonoBehaviour
{
    private bool checkpointActive = false;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Color inactiveColor = Color.darkSeaGreen;
    [SerializeField] private Color activeColor = Color.mediumSpringGreen;
    
    private static Checkpoint_Script currentActiveCheckpoint;

    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = inactiveColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCheckpoint_Script checkpoint = other.GetComponent<PlayerCheckpoint_Script>();

        if (checkpoint != null)
        {
            ActivateCheckpoint();
            checkpoint.SetCheckpoint(transform.position);
        }
    }

    private void ActivateCheckpoint()
    {
        if (currentActiveCheckpoint != null)
        {
            currentActiveCheckpoint.Deactivate();
        }

        currentActiveCheckpoint = this;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = activeColor;
        }

        Debug.Log("Checkpoint activé");
    }

    private void Deactivate()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = inactiveColor;
        }
    }
}