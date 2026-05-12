using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 5f;
    public Transform couche1;
    public Transform couche2;
    public Transform couche3;
    public Transform couche4;
    public float spinSpeed = 22f;

    private Transform currentTarget;

    void Start()
    {
        // On définit la première destination
        currentTarget = endPoint;
    }

    void Update()
    {
        // MoveTowards garantit une vitesse fixe (en unités par seconde)
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // On vérifie si on est arrivé au point
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
        {
            // On change de cible pour le prochain Update
            currentTarget = (currentTarget == endPoint) ? startPoint : endPoint;

           
        }

        couche1.Rotate(Vector3.forward * spinSpeed * 1.5f * Time.deltaTime);
        couche2.Rotate(Vector3.forward * spinSpeed*2 * Time.deltaTime);
        couche3.Rotate(Vector3.forward * spinSpeed*3 * Time.deltaTime);
        couche4.Rotate(Vector3.forward * spinSpeed*4 * Time.deltaTime);
    }

  
}