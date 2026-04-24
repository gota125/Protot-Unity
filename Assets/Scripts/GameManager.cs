using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public  class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public int enemiesDestroyed = 0;
   public bool gameOver = false;
   public int playerHealth = 3;
   public Image vie1;
   public Image vie2;
   public Image vie3;
   public bool godMode = false;

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
      if (playerHealth == 3)
      {
         vie1.enabled = true;
         vie2.enabled = true;
         vie3.enabled = true;
      }
      else if (playerHealth == 2)
      {
         vie1.enabled = true;
         vie2.enabled = true;
         vie3.enabled = false;
      }
      else if (playerHealth == 1)
      {
         vie1.enabled = true;
         vie2.enabled = false;
         vie3.enabled = false;
      }
      else if (playerHealth == 0)
      {
         vie1.enabled = false;
         vie2.enabled = false;
         vie3.enabled = false;
      }
   }
   public void PlayerTakeDamage()
   {
      playerHealth--;
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
         Debug.Log("Game Over");
      }
   }
   

   public void RestartGame()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   void GodMode()
   {
      if (Input.GetKeyDown(KeyCode.G))
      {
          if (godMode == false)
          {
             playerHealth = 1000;
             godMode  = true;
                  
          }
                        
          else if (godMode == true &&  playerHealth >= 4)
          {
             playerHealth = 3;
             godMode  = false;
          }
      }
     
   }
}
