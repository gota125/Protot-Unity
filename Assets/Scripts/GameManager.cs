using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public  class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public int enemiesDestroyed = 0;
   public bool gameOver = false;
   public int playerHealth = 3;
   public int playerMaxHealth = 3;
   public Image vie1;
   public Image vie2;
   public Image vie3;
   public Image vie4;
   public Image vie5;
   public bool godMode = false;
   private static bool isInvincible = false;
   public static float invincibleCooldown = 0.3f;

   private void Awake()
   {
      gameOver = false;
      
      if (Instance != null && Instance != this)
      {
         Destroy(gameObject);
      }
      else
      {
         {
            Instance = this;
         }
      }
   }
   
   /*
   void Start() {
// Hides the cursor
      Cursor.visible = false;
   }
   */
   
   void Update()
   {
      HealthManager();
      PlayerDead();
      GodMode();
      
      if (Input.GetKeyDown(KeyCode.R))
      {
         RestartGame();
         playerHealth = 3;
      }
   }

   public void HealthManager()
   {
      if (playerHealth == 5)
      {
         vie1.enabled = true;
         vie2.enabled = true;
         vie3.enabled = true;
         vie4.enabled = true;
         vie5.enabled = true;
      }
      else if (playerHealth == 4)
      {
         vie1.enabled = true;
         vie2.enabled = true;
         vie3.enabled = true;
         vie4.enabled = true;
         vie5.enabled = false;
      }
      else if (playerHealth == 3)
      {
         vie1.enabled = true;
         vie2.enabled = true;
         vie3.enabled = true;
         vie4.enabled = false;
         vie5.enabled = false;
      }
      else if (playerHealth == 2)
      {
         vie1.enabled = true;
         vie2.enabled = true;
         vie3.enabled = false;
         vie4.enabled = false;
         vie5.enabled = false;
      }
      else if (playerHealth == 1)
      {
         vie1.enabled = true;
         vie2.enabled = false;
         vie3.enabled = false;
         vie4.enabled = false;
         vie5.enabled = false;
      }
      else if (playerHealth == 0)
      {
         vie1.enabled = false;
         vie2.enabled = false;
         vie3.enabled = false;
         vie4.enabled = false;
         vie5.enabled = false;
      }

      if (playerHealth >= playerMaxHealth)
      {
         playerHealth = playerMaxHealth;
      }
      
      
      
   }

   public void LifeUpgrade()
   {
      playerMaxHealth++;
      playerHealth++;
   }

   public void Life()
   {
      playerHealth++;
   }
   
   
   
   public void PlayerTakeDamage()
   {
      if (isInvincible)
      {
         return;
      }
      playerHealth--;
      
      StartCoroutine(Invincibility());
      
   }

   public void AddEnemyKill()
   {
      if(!gameOver)
      {
         enemiesDestroyed++;
         Debug.Log("enemmi kill :"+enemiesDestroyed);
      }
   }
   public void PlayerDead()
   {
      if (playerHealth <= 0)
      {
         gameOver = true;
      }
   }
   

   public void RestartGame()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
   
   public void ResetPlayer()
   {
      gameOver = false;
      playerHealth = playerMaxHealth;
   }

   void GodMode()
   {
      if (Input.GetKeyDown(KeyCode.G))
      {
          if (godMode == false)
          {
             playerHealth = 1000;
             
             PlayerMovement.Instance.runSpeed = 10;
             PlayerMovement.Instance.dashCoolDown = 0;
             Parry.Instance.cooldownTime = 0;
             godMode  = true;


          }
                        
          else if (godMode == true )
          {
             Debug.Log("godmode false");
             playerHealth = 3;
             godMode  = false;
             PlayerMovement.Instance.runSpeed =  5f ;
             PlayerMovement.Instance.dashCoolDown = 1f;
             Parry.Instance.cooldownTime = 1.5f;
          }
      }
     
   }
   
   public static IEnumerator Invincibility()
   {
      isInvincible = true;

      yield return new WaitForSeconds(invincibleCooldown);

      isInvincible = false;
   }
}
