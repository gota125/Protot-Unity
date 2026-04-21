using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 speed;

    void Start()
    {
        Destroy(this.gameObject,5);
    }
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
