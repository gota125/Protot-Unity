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
        if (other.gameObject.tag == "CooldownUpgrade")
        {
            Parry.Instance.CooldownUpgrade();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "SpeedUpgrade")
        {
            PlayerMovement.Instance.SpeedUpgrade();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "LifeUpgrade")
        {
            GameManager.Instance.LifeUpgrade();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Vie")
        {
            GameManager.Instance.Life();
            Destroy(other.gameObject);
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
