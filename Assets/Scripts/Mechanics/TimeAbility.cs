using UnityEngine;

public class TimeAbility : MonoBehaviour
{
    [Header("Time Settings")]
    public float slowTimeScale = 0.2f;
    public float duration = 2f;
    public float cooldownTime = 5f;

    [Header("Status (Read Only)")]
    public bool isSlowed = false;
    public bool isOnCooldown = false;
    
    private float abilityTimer;
    private float cooldownTimer;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && !isSlowed && !isOnCooldown)
        {
            StartCoroutine(ActivateTimeSlow());
        }

        
        if (isOnCooldown)
        {
            cooldownTimer -= Time.unscaledDeltaTime;
            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
            }
        }
    }

    System.Collections.IEnumerator ActivateTimeSlow()
    {
        isSlowed = true;
        Time.timeScale = slowTimeScale;
        
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

       
        yield return new WaitForSecondsRealtime(duration);

     
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        isSlowed = false;

       
        isOnCooldown = true;
        cooldownTimer = cooldownTime;
    }
}