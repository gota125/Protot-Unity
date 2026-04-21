using Unity.VisualScripting;
using UnityEngine;

public class Parry : MonoBehaviour
{
   void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Projectile" && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Parry");
            
        }
    }
}
