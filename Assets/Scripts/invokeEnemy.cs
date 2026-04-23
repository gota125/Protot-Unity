using System;
using System.Numerics;
using UnityEngine;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;

public class invokeEnemy : MonoBehaviour
{
   [SerializeField] private GameObject Enemy;

   private void Start()
   {
      Instantiate(Enemy, new Vector3(0,0,0), transform.rotation);
   }
}
