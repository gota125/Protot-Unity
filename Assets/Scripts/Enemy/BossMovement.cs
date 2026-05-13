using System.Collections.Generic;
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
    public List<Transform> waypoints;
    public float waitTime = 1f;
    private int currentWayIndex = 0;
    private bool isWaiting = false;

    private Transform currentTarget;

    void Start()
    {
        // On définit la première destination
        currentTarget = endPoint;
    }

    void Update()
    {
        couche1.Rotate(Vector3.forward * spinSpeed * 1.5f * Time.deltaTime);
        couche2.Rotate(Vector3.forward * spinSpeed*2 * Time.deltaTime);
        couche3.Rotate(Vector3.forward * spinSpeed*3 * Time.deltaTime);
        couche4.Rotate(Vector3.forward * spinSpeed*4 * Time.deltaTime);
        if(waypoints == null || waypoints.Count == 0|| isWaiting)return;
        Transform target = waypoints[currentWayIndex];
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
           StartCoroutine(WaitAtPoint());
        }
        
        

        
    }

    System.Collections.IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        
        currentWayIndex = (currentWayIndex + 1) % waypoints.Count;
        isWaiting = false;
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count < 2)return;
        
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[i] != null)
            {
                Vector3 current = waypoints[i].position - transform.position;
                Vector3 next = waypoints[(i + 1) % waypoints.Count].position;
                
                
            }
        }
    }
  
}