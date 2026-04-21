using System;
using Unity.VisualScripting;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public bool isParry = false;
    [SerializeField] private float speed = 10f;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isParry = true;
            
        }
        else
        {
            isParry = false;
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
                }

                isParry = false;
            
        }
    }

