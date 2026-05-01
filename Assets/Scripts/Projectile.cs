using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 speed;
    public GameObject owner;
    public float lifeTime;
    

    void Start()
    {
        Destroy(this.gameObject,lifeTime);
        transform.rotation = owner.transform.rotation;
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
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Ennemy")
        {
            Destroy(gameObject);
        }
    }
    
}
