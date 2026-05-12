using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float moveSpeed = 2f;
    public float waitTime = 1f;

    private Vector3 targetPoint;
    private bool isWaiting = false;

    private Vector3 lastPosition;
    public Vector3 PlatformVelocity { get; private set; }

    public GameObject GateA;
    public GameObject GateB;
    public GameObject PlatformA;
    public GameObject PlatformB;

    void Start()
    {
        transform.position = pointA.position;
        targetPoint = pointB.position;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (!isWaiting)
        {
            MovePlatform();
        }

        // vraie vitesse
        PlatformVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
        lastPosition = transform.position;
    }

    void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint,
            moveSpeed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, targetPoint) < 0.01f)
        {
            StartCoroutine(WaitAndSwitch());
        }
    }

    IEnumerator WaitAndSwitch()
    {
        isWaiting = true;

        if (targetPoint == pointA.position)
        {
            GateA.SetActive(false);
            PlatformA.SetActive(false);
        }

        if (targetPoint == pointB.position)
        {
            GateB.SetActive(false);
            PlatformB.SetActive(false);
        }

        yield return new WaitForSeconds(waitTime);

        targetPoint = targetPoint == pointA.position
            ? pointB.position
            : pointA.position;

        isWaiting = false;

        GateA.SetActive(true);
        PlatformA.SetActive(true);
        GateB.SetActive(true);
        PlatformB.SetActive(true);
    }
}