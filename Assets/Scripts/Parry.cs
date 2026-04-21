using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Parry : MonoBehaviour
{
    public bool isParry = false;
    [SerializeField] private float speed = 10f;
    public float timer;
    private float CooldownTime = 2;
    public bool isCooldown = false;
    public Image CooldownImage;

    void Start()
    {
        CooldownImage.fillAmount = 0f;
        
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)&& isCooldown == false)
        {
            isParry = true;
            
            
        }
        else
        {
            isParry = false;
            ApplyCooldown();
            Debug.Log(" parry is not ready");
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

                    projectile.speed = mouseToWorld*speed;
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
            Debug.Log("ready to parry");
        }
    }

    void ExecuteAction()
    {
        isCooldown =  true;
        timer = CooldownTime;
        CooldownImage.fillAmount = 1f;
    }
    }

