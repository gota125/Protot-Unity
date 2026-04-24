using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_parry : MonoBehaviour
{
  public bool enemieParry = false;
public float probalityToParry=50f;


  private void Update()
  {
    
    
    float probality = Random.Range(0, 100);
    if (probality <= probalityToParry)
    {
      Debug.Log("parry enemie");
      enemieParry = true;
    }
    else
    {
      Debug.Log(" no parry enemie");
    }
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    
    if (other.tag == "Projectile" && enemieParry)
    {
      
     Projectile projectile = other.gameObject.GetComponent<Projectile>();

     Vector2 direction = - projectile.transform.position;
     projectile.speed = direction.normalized * projectile.speed;




    }
                    
            
  }

  
}
