using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Parry : MonoBehaviour
{
    public bool isParry = false;
    [SerializeField] private float speed = 10f;
    public float timer;
    public float CooldownTime = 2;
    public bool isCooldown = false;
    public Image CooldownImage;
    public SpriteRenderer spriteParry;
    

    void Start()
    {
        CooldownImage.fillAmount = 1f;
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isCooldown == false)
        {
            StartParry();
        }

        if (isParry)
        {
            spriteParry.enabled = true;
        }
        else
        {
            spriteParry.enabled = false;
            ApplyCooldown();
        }
    }

    void OnTriggerStay2D(Collider2D other)
        {
            Vector3 mousePos = Input.mousePosition;
                    Vector3 mouseToWorld = Camera.main .ScreenToWorldPoint(mousePos);
                    mouseToWorld.z = 0;


                    if (other.tag == "Projectile" && isParry)
                    {
                        Debug.Log("Parry");

                        Projectile projectile = other.gameObject.GetComponent<Projectile>();

                        Vector2 direction = mouseToWorld - projectile.transform.position;

                        projectile.speed = direction.normalized * speed;
                        
                        projectile.owner = gameObject; //Change le tag du projectile comme ca sa tue l'ennemi auqnd ca revient

                        ExecuteAction();
                    }
                    
            
        }

    void ApplyCooldown()
    {
        timer -= Time.deltaTime;
        CooldownImage.fillAmount = timer/CooldownTime;
        if (timer <= 0)
        {
            isCooldown = false;
            CooldownImage.fillAmount = 1f;
            
        }
    }

    void ExecuteAction()
    {
        isCooldown =  true;
        timer = CooldownTime;
        CooldownImage.fillAmount = 1f;
    }
    
    
    void StartParry() // Fonction et coroutine pour empecher de maintenir le clic de parry 
    {
        isParry = true;
        ExecuteAction();
        StartCoroutine(ParryDuration());
    }

    IEnumerator ParryDuration()
    {
        yield return new WaitForSeconds(0.02f);
        isParry = false;
    }
}



