using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Melee_Enemy_Script2 : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject enemy;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    public Image attackBar;
    [SerializeField] private float speed;
    [SerializeField] private float attackRadius = 0.1f;
    [SerializeField] private float StopNearPlayer;
    [SerializeField] private float isNearPlayer;
    [SerializeField] private float attackSpeed;
    [SerializeField] private bool activatedVariant = false;
    private Vector3 enemyToPlayer;
    private float enemyToPlayerDist;
    public float attackTimer;

    private bool canMove = true;
    private bool StartMove = false;
    private bool isAttacking = false;

    void Update()
    {
        enemyToPlayer = player.transform.position - enemy.transform.position;
        enemyToPlayerDist = Vector2.Distance(enemy.transform.position, player.transform.position);
        EnemyToPlayer();
        if (activatedVariant)
        {
            EnemyMOvement();
            Attack();
            VarCanMove();

        }
        else
        {
            EnemyMOvement();
            Attack();
            CanMove(); 
        }
        
    }
    private void EnemyToPlayer()
    {
        Vector2 top = Vector2.up;
        
        float angle = Vector2.Angle(enemyToPlayer, top);
        
        Vector3 cross = Vector3.Cross(enemyToPlayer, top);
        
        if(cross.z > 0)
            enemy.transform.eulerAngles = new Vector3(0, 0, -angle);
        else
            enemy.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    private void EnemyMOvement()
    {
        if(canMove)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void Attack()
    {
        if (enemyToPlayerDist < attackRadius )
        {
            Debug.Log($"Enemy can attack. Dist is {enemyToPlayerDist}");
            if(!isAttacking)
                StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        float time = 0;
        while (time < attackTimer)
        {
            yield return null;
            time += Time.deltaTime;
            attackBar.fillAmount = time / attackTimer;
        }

        if (enemyToPlayerDist < attackRadius)
        {
            print("Attack !");
            InstatiateAttack();
        }
        attackBar.fillAmount = 0;
        isAttacking = false;
    }

    private void CanMove()
    {
        if (enemyToPlayerDist < isNearPlayer)
        {
            StartMove = true;
        }
        
        if (enemyToPlayerDist > StopNearPlayer && StartMove == true)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    private void VarCanMove()
    {
        if (enemyToPlayerDist > StopNearPlayer && enemyToPlayerDist < isNearPlayer)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
    
    private void InstatiateAttack()
    {
            GameObject spawnedMissile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

            Projectile projectile = spawnedMissile.GetComponent<Projectile>();
            projectile.speed = spawnPoint.up * attackSpeed;
            
            projectile.owner = gameObject;
    }
    void Die()
    {
        GameManager.Instance.AddEnemyKill();
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Projectile proj = other.gameObject.GetComponent<Projectile>();

        if (proj != null && proj.owner != gameObject)
        {
            Die();
        }
    }
}
