using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FollowCooldown : MonoBehaviour
{
   
    public Image cooldownImage;
    public GameObject viseur;
    
    void Update()
    {
        FollowCursor();
    }
    
    
    private void FollowCursor()
    {
        cooldownImage.transform.position = viseur.transform.position;
    }
}
