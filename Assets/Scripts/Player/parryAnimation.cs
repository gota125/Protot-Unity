using System;
using Unity.VisualScripting;
using UnityEngine;
using Math = Unity.Mathematics.Geometry.Math;

public class parryAnimation : MonoBehaviour
{
    [SerializeField] private GameObject self;
    [SerializeField] private Transform rotationAnchor;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float speed;
    [SerializeField] private float maxangle;
    void Update()
    {
        parrylocalisation();
        Debug.Log(rotationAnchor.eulerAngles.z);
        Debug.Log(self.transform.eulerAngles.z);
    }

    private void Start()
    {
        
    }

    private void parrylocalisation()
    {
        
        self.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationAnchor.eulerAngles.z - 180);
    }

}
