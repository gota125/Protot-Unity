using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            GameManager.Instance.PlayerTakeDamage();
        }
        
    }

     private void Update()
    {
        if (GameManager.Instance.gameOver == true)
        {
            Destroy(gameObject); 
            Debug.Log("Game Over");
        }
        
    }
}
