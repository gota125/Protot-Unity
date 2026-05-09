using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 speed;
    public float lifeTime = 3f;
    
    [Header("Status")]
    public GameObject owner;
    
    private SpriteRenderer sr;
    private TrailRenderer tr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();

        // Sécurité : évite une erreur si le projectile est spawn sans owner
        if (owner != null)
        {
            transform.rotation = owner.transform.rotation;
        }

        Destroy(gameObject, lifeTime);
    }
    
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.position += speed * Time.deltaTime;
    }

    // Fonction appelée par le script de Parade
    public void ChangeOwnership(GameObject newOwner, Color newColor)
    {
        owner = newOwner;
        
        // Change la couleur du Sprite
        if (sr != null) sr.color = newColor;

       
        // Petit boost visuel : on peut grossir  le projectile paré
        transform.localScale *= 1.2f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // On ignore les collisions avec celui qui a tiré le projectile
        if (other.gameObject == owner)
            return;

        
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ennemy"))
        {
          
            Destroy(gameObject);
        }
    }
}