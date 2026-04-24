using UnityEngine;

public class melleattack : MonoBehaviour
{
    public Vector3 speed;
    public GameObject owner;
    

    void Start()
    {
        Destroy(this.gameObject,5);
    }
    
    void Update()
    {
        Movement();
        
        
    }

    public void Movement()
    {
        transform.position = transform.position + speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject == owner)
            return;

        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        
    }

}
