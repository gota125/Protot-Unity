using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 speed; 
   

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        
    }

    public void Movement()
    {
        transform.position = transform.position + speed * Time.deltaTime;
    }
}
