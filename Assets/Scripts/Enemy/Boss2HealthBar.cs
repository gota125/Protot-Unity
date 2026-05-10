using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss2HealthBar : MonoBehaviour
{
    public GameObject healthBarPosition;
    public Canvas healthcanvas;
    public Image ennemyHealth;  
    public Image ennemyHealthBar;  
    public MiniBoss2_Script selfEnemy;
    
   
    
    
    void Update()
    {
        FollowCursor();
        UpdateHealthBar();
        ShowHealthBar();
        
    }
    
    private void UpdateHealthBar()
    {
        ennemyHealth.fillAmount = (float)selfEnemy.currentHealth / selfEnemy.bossMaxHealth;
    }
    
    
    private void FollowCursor()
    {
        healthBarPosition.transform.position = healthcanvas.transform.position;
    }
    
    private void ShowHealthBar()
    {
        ennemyHealth.gameObject.SetActive(selfEnemy.currentHealth < selfEnemy.bossMaxHealth);
        ennemyHealthBar.gameObject.SetActive(selfEnemy.currentHealth < selfEnemy.bossMaxHealth);
    }
}