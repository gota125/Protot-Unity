using UnityEngine;

public class PlayerCheckpoint_Script : MonoBehaviour
{
    private Vector3 currentCheckpoint;
    private Rigidbody2D rb;

    private void Start()
    {
        currentCheckpoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        currentCheckpoint = checkpointPosition;
        Debug.Log("Checkpoint activé");
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        GameManager.Instance.ResetPlayer();
    }
}
