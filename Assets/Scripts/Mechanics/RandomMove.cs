using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMove : MonoBehaviour
{
    public float speed = 3f;
    public float changeDirectionDelay = 2f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private float timer;
    public GameObject self;
    public RigidbodyConstraints2D constraints;
    private bool isMoving;
    
    private EnemyScript enemyScript;


    private void Start()
    {
        enemyScript = GetComponent<EnemyScript>();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(MoveCycle());
    }
    
    

    void Update()
    {
        if (!enemyScript.HasDetectedPlayer)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }

        timer += Time.deltaTime;
        Debug.Log(timer);
        Debug.Log(changeDirectionDelay);

        if (timer >= changeDirectionDelay)
        {
            ChooseNewDirection();
            timer = 0;
           
        }

        rb.constraints = isMoving
            ? RigidbodyConstraints2D.None
            : RigidbodyConstraints2D.FreezeAll;
    }
    void FixedUpdate()
    {
        if (!enemyScript.HasDetectedPlayer)
            

        if (isMoving)
        {
            rb.MovePosition(rb.position + movementDirection * speed * Time.fixedDeltaTime);
           
        }
    }

 void  ChooseNewDirection()
    {
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;;
        movementDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle) );
        
        if (movementDirection.x > 0)transform.localScale=new Vector3(-1, 1, 1);
        else if (movementDirection.x < 0)transform.localScale = new Vector3(1, 1, 1);
    }
 
 
    IEnumerator MoveCycle()
    {
        while (true)
        {
            isMoving = true;
            
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            isMoving = false;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
 
}
