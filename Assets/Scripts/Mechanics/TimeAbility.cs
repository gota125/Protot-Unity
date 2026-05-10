using System;
using UnityEngine;

public class TimeAbility : MonoBehaviour
{
    [Header("Réglages du time slow")]
    public float slowTimeScale = 0.2f;
    public float normalTimeScale = 1.0f;
    
    private bool isSlowed = false;

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleTimeSlow();
        }   
    }

    void ToggleTimeSlow()
    {
        isSlowed = !isSlowed;
        if (isSlowed)
        {
            Time.timeScale = slowTimeScale;
            Time.fixedDeltaTime = 0.02f *Time.timeScale;
        }
        else
        {
            Time.timeScale = normalTimeScale;
            Time.fixedDeltaTime = 0.02f ;
        }
    }
}
