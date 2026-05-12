using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public PlatformScript platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered platform");
            PlayerMovement.Instance.SetCurrentPlatform(platform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.Instance.SetCurrentPlatform(null);
        }
    }
}
