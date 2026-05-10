using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public float range;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shootlaser();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void Shootlaser()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position); // Point de départ

        // On lance le rayon invisible
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.up, range);

        if (hit.collider != null)
        {
            // Si on touche quelque chose, le laser s'arrête là
            lineRenderer.SetPosition(1, firePoint.position + firePoint.up * range);
 
        }
        else
        {
            
            lineRenderer.SetPosition(1, firePoint.position + firePoint.up * range);
        }
    }
}
