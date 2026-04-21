using UnityEngine;


public class ParryDirection : MonoBehaviour
{
    public Transform player;
    public float distance = 2F;
    Rigidbody2D rb;

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
        
        if(player == null)return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 direction = (mousePos - player.position).normalized;
        transform.position = (Vector2)player.position + (direction * distance);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

   
}
