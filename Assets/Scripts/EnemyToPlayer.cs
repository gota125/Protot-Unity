using UnityEngine;

public class EnemyToPlayer : MonoBehaviour
{
    
    public Transform enemy;
    public float distance = 2F;
    Rigidbody2D rb;
    public Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        Parrydirection();  
    }

    public void Parrydirection()
    {
        
        if(enemy == null)return;
        
        Vector2 direction = ( player.position- enemy.position).normalized;
        transform.position = (Vector2)enemy.position + (direction * distance);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }


    
}
