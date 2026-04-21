using UnityEngine;
using UnityEngine.SceneManagement;

public  class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public int enemiesDestroyed = 0;
   public bool gameOver = false;

   private void Awake()
   {
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
      if (!gameOver)
      {
         gameOver = true;
         Debug.Log("gameOver");
      }
   }

   void Update()
   {
      if (gameOver && Input.GetKeyDown(KeyCode.R))
      {
         RestartGame();
      }
   }

   public void RestartGame()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
