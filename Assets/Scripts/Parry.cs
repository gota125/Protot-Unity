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
        CooldownImage.fillAmount = 0f;
        
    }
    private void Update()
    {

         
        if (Input.GetMouseButton(0)&& isCooldown == false)
        {
            isParry = true;
            
            
            
        }
        else
        {
            isParry = false;
            ApplyCooldown();
            
        }

        if (isParry)
        {
           spriteParry.enabled = true;
        }
        else 
        {
            
           spriteParry.enabled = false;
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
                    Vector2 direction = mouseToWorld-projectile.transform.position;
                    projectile.speed = direction.normalized*speed;
                    print(projectile.speed);
                    ExecuteAction();
                    
                }

                isParry = false;
                
            
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
    }

