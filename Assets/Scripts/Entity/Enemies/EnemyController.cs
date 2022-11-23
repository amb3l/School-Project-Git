using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class EnemyController : MonoBehaviour
{
    protected Rigidbody2D enemyRB;
    protected Animator enemyAnimator;
    protected Vector2 startPoint;
    protected EnemyState currentState;

    protected float lastStateChange;
    protected float timeToNextChange;

    [SerializeField] private float maxStateTime;
    [SerializeField] private float minStateTime;
    [SerializeField] private EnemyState[] availableStats; 

    [Header("Movement")]
    [SerializeField] protected float speed;
    [SerializeField] private float range;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;

                     protected bool faceRight = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        transform.position = Align(transform.position.x, transform.position.y);
        startPoint = transform.position;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (IsGroundEnding() || IsWallNear())
        {
            Flip();
        }

        if (currentState == EnemyState.Move)
        {
            Move();
        }
    }

    protected virtual void Update()
    {
        if (Time.time - lastStateChange > timeToNextChange)
        {
            GetRandomState();
        }
    }

    protected virtual void Move()
    {
        enemyRB.velocity = transform.right * new Vector2(speed, enemyRB.velocity.y);
    }

    protected virtual void Stop()
    {
        enemyRB.velocity = new Vector2(0, enemyRB.velocity.y);
    }

    protected virtual void Flip()
    {
        Stop();
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(range * 2, 5f, 0));
    }

    private bool IsGroundEnding()
    {
        return !Physics2D.OverlapPoint(groundCheck.position, groundLayer);
    }

    private bool IsWallNear()
    {
        return Physics2D.OverlapPoint(wallCheck.position, groundLayer);
    }

    protected void GetRandomState()
    {
        int state;
        do
        {
            state = Random.Range(0, availableStats.Length);
        } while (availableStats[state] == EnemyState.Idle && currentState == EnemyState.Idle);
        
        timeToNextChange = Random.Range(minStateTime, maxStateTime);

        if (currentState != availableStats[state])
        {
            ChangeState(availableStats[state]);
        }
        lastStateChange = Time.time;
    }

    protected virtual void ChangeState(EnemyState state) 
    {   
        enemyAnimator.SetBool(currentState.ToString(), false);
        currentState = state;
        enemyAnimator.SetBool(state.ToString(), true);
    }

    private Vector2 Align(float x, float y)
    {
        x = (float)(System.Math.Ceiling(x) - 0.5);
        y = (float)(System.Math.Ceiling(y) - 0.5);
        return new Vector2(x, y);
    }
}

public enum EnemyState
{
    Idle,
    Move,
    Attack
}
