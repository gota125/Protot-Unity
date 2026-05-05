using System;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
[SerializeField] public bool locked;
public BoxCollider2D Pont;
public SpriteRenderer Sprite;

void Start()
{
    Pont = gameObject.GetComponent<BoxCollider2D>();
    locked = false;
    Pont.enabled = true;
    Sprite.enabled = true;
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( Input.GetKeyDown(KeyCode.E) )
        {
            locked = true;
            Pont.enabled = false;
            Sprite.enabled = false;
        }
        
    }
   
}
