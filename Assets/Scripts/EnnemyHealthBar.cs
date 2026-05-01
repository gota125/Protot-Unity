using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnnemyHealthBar : MonoBehaviour
{
    public GameObject healthBarPosition;
    public Canvas healthcanvas;
    public Image ennemyHealth;  
    public Image ennemyHealthBar;  
    public EnemyScript selfEnemy;
   
    
    
    void Update()
    {
        FollowCursor();
        UpdateHealthBar();
        ShowHealthBar();
        
    }
    
    private void UpdateHealthBar()
    {
        ennemyHealth.fillAmount = (float)selfEnemy.ennemyHealth / selfEnemy.ennemyMaxHealth;
    }
    
    
    private void FollowCursor()
    {
        healthBarPosition.transform.position = healthcanvas.transform.position;
    }
    
    private void ShowHealthBar()
    {
        ennemyHealth.gameObject.SetActive(selfEnemy.ennemyHealth < selfEnemy.ennemyMaxHealth);
        ennemyHealthBar.gameObject.SetActive(selfEnemy.ennemyHealth < selfEnemy.ennemyMaxHealth);
    }
}
