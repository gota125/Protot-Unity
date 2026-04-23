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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewDirection();




    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeDirectionDelay)
        {
            ChooseNewDirection();
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDirection * speed * Time.fixedDeltaTime);
    }

 void  ChooseNewDirection()
    {
        float randomAngle = Random.Range(0, 360);
        movementDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle) );
        
        if (movementDirection.x > 0)transform.localScale=new Vector3(-1, 1, 1);
        else if (movementDirection.x < 0)transform.localScale = new Vector3(1, 1, 1);
    }
 
}
