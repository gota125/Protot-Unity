using UnityEngine;

public class PressureButton : MonoBehaviour
{
    public MovingPlatform platform; // Glisse la plateforme ici dans l'inspecteur
    public Color activeColor = Color.green;
    public Color idleColor = Color.red;
    
    private SpriteRenderer sr;
    private int objectsOnButton = 0; // Pour gérer si plusieurs objets sont dessus

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = idleColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Projectile")) 
        {Debug.Log("Entrée détectée : " + other.name + " avec le tag : " + other.tag);
            objectsOnButton++;
            UpdateStatus();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Projectile"))
        {
            objectsOnButton--;
            UpdateStatus();
        }
    }
    

    void UpdateStatus()
    {
        if (objectsOnButton > 0)
        {
            sr.color = activeColor;
            platform.SetMoving(true);
        }
        else
        {
            sr.color = idleColor;
            platform.SetMoving(false);
        }
    }
}