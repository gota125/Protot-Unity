using System;
using UnityEngine;

public class Melee_Enemy_Script : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject enemy;
    [SerializeField] private float speed;
    [SerializeField] private float attackRadius = 0.1f;
    [SerializeField] private float StopNearPlayer;
    [SerializeField] private float isNearPlayer;
    [SerializeField] private bool activatedVariant = false;
    private Vector3 enemyToPlayer;
    private float enemyToPlayerDist;

    private bool canMove = true;
    private bool StartMove = false;

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
        }
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
}
