using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;     // Le joueur
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 0, 0);
    public  float deadzone = 5f;
    
    
    [Header("Mouse Influence")]
    public float mouseInfluence = 2f; // Jusqu'où la caméra peut s'éloigner vers la souris

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Récupérer la position de la souris dans le monde
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10;

        // 2. Calculer la direction entre le joueur et la souris
        Vector3 lookDirection = mousePos - target.position;

        // 3. Calculer la position désirée
        // On part du joueur + l'offset, et on ajoute une fraction de la distance vers la souris
       float distance = Mathf.Clamp(lookDirection.magnitude/deadzone ,0,mouseInfluence);
       Vector3 targetPosition = target.position + offset + lookDirection.normalized * distance;
        // 4. (Lerp)
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothedPosition;
    }
}