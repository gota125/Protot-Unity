using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private bool noMoreEnemy = false;
    private bool PlayerOnEndLevel = false;
    
    void Update()
    {
        detectedIfEnemy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        endOfLEvel();
    }

    private void detectedIfEnemy()
    {
        if (FindAnyObjectByType<EnemyScript>(FindObjectsInactive.Include).IsUnityNull())
        {
            noMoreEnemy = true;
        }
    }

    private void endOfLEvel()
    {
        if (noMoreEnemy)
        {
            SceneManager.LoadScene("first scene protot");
        }
    }
}
