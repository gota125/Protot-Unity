using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
     void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            GameManager.Instance.PlayerDead();
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
        
    }
}
