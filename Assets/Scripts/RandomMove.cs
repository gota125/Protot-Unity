using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMove : MonoBehaviour
{
    private float randomx;
    private float randomy;
public Transform player;
    void Update()
    {
     
player.position=Vector3.Max(Vector3.left, Vector3.down);
    }

    

   

   
}
