using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 3f;
    private bool shouldMove = false;

    void Update()
    {
        // Si le bouton a été activé
        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
        }
        else
        {
            
        }
    }

    public void SetMoving(bool state)
    {
        shouldMove = state;
    }
}
