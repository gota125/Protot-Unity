using System;
using Unity.VisualScripting;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public bool isParry = false;

    private void FixedUpdate()
    {
        if (isParry)
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isParry = true;


                if (other.tag == "Projectile" && isParry)
                {
                    Debug.Log("Parry");
                    Projectile projectile = other.gameObject.GetComponent<Projectile>();

                    projectile.speed = -projectile.speed;
                }

                isParry = false;
            }
        }
    }

