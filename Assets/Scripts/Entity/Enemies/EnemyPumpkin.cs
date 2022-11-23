using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPumpkin : EnemyController
{
    [SerializeField] float attackDistance;
    protected MovementController player;
    protected bool isAngry;
    protected bool isAttacking;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<MovementController>();
        StartCoroutine(ScanForPlayer());
    }

    protected override void Update()
    {
        if (isAngry)
        {
            return;
        }
        base.Update();
        
    }

    protected override void ChangeState(EnemyState state)
    {
        base.ChangeState(state);
        switch (currentState)
        {
            case EnemyState.Idle:
                enemyRB.velocity = Vector2.zero;

                break;
            case EnemyState.Move:
                startPoint = transform.position;
                break;

            case EnemyState.Attack:
                isAttacking = true;
                Attack();
                break;
        }
    }

    //---------
    protected virtual void Attack()
    {
        enemyRB.velocity = transform.right * new Vector2(speed*2.5f, enemyRB.velocity.y);
    } 
    //-------
    protected IEnumerator ScanForPlayer()
    {
        while (true)
        {
            CheckPlayerInRange();
            yield return new WaitForSeconds(0.2f);
        }
    }
    //-------
    protected void CheckPlayerInRange()
    {
        if(player == null)
        {
            return;
        }

        if ( (Vector3.Distance(transform.position, player.transform.position) < attackDistance) 
            && ((player.transform.position.y - transform.position.y > -1) 
                && (player.transform.position.y - transform.position.y < 2)) )
        {
            isAngry = true;
            TurnToPlayer();
            ChangeState(EnemyState.Attack);
            lastStateChange = 0;    
        }
        else
        {
            isAngry = false;
        }

    }
    //=
    protected void TurnToPlayer()
    {
        if (player.transform.position.x - transform.position.x > 0 && !faceRight)
            Flip();
        else if (player.transform.position.x - transform.position.x < 0 && faceRight)
            Flip();
    }
}
