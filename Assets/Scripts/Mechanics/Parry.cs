using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Parry : MonoBehaviour
{
    public static Parry Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private float reflectSpeed = 15f;
    [SerializeField] private float parryWindow = 0.15f; 
    public float cooldownTime = 2f;
    public Color parryColor = Color.cyan;

    [Header("References")]
    public Image cooldownImage;
    public SpriteRenderer spriteParry;

    [Header("State")]
    public bool isParry = false;
    public bool isCooldown = false;
    private float timer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (cooldownImage != null) cooldownImage.fillAmount = 0f;
    }

    void Update()
    {
        // On ne peut parer que si on n'est pas en cooldown
        if (Input.GetMouseButtonDown(0) && !isCooldown)
        {
            StartParry();
        }

        if (isCooldown)
        {
            ApplyCooldown();
        }
        
        // Visuel de la parade
        spriteParry.enabled = isParry;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isParry && other.CompareTag("Projectile"))
        {
            Projectile projectile = other.GetComponent<Projectile>();

            if (projectile != null)
            {
                Debug.Log("Projectile Paré !");

                // Calcul de la direction vers la souris
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;
                Vector2 direction = (mouseWorldPos - projectile.transform.position).normalized;

                // Application de la nouvelle vitesse
                projectile.speed = direction * reflectSpeed;

                // APPEL DE TA NOUVELLE FONCTION
                projectile.ChangeOwnership(gameObject, parryColor);

                // On arrête la parade immédiatement après un succès pour éviter de parer 50 fois
                isParry = false; 
                
                // On peut aussi déclencher un petit effet ici (Camera Shake ?)
            }
        }
    }

    void StartParry()
    {
        isParry = true;
        isCooldown = true;
        timer = cooldownTime;
        StartCoroutine(ParryDurationRoutine());
    }

    IEnumerator ParryDurationRoutine()
    {
        yield return new WaitForSeconds(parryWindow);
        isParry = false;
    }

    void ApplyCooldown()
    {
        timer -= Time.deltaTime;
        
        if (cooldownImage != null)
        {
            // Inversion du fillAmount pour que l'image se vide (ou se remplisse)
            cooldownImage.fillAmount = timer / cooldownTime;
        }

        if (timer <= 0)
        {
            isCooldown = false;
            if (cooldownImage != null) cooldownImage.fillAmount = 0f;
        }
    }

    public void CooldownUpgrade(float reduction)
    {
        cooldownTime = Mathf.Max(0.2f, cooldownTime - reduction);
    }
}