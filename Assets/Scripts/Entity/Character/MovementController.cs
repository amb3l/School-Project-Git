using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class MovementController : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;

    [Header("Colliders")]
    [SerializeField] private CapsuleCollider2D topCollider;
    [SerializeField] private CapsuleCollider2D bottomCollider;

    [Header("Horizontal Movement")]
    [SerializeField] private float speed;

    [Header("Jumping and Crawling")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float jumpForce;
    [SerializeField] private float hangTime;
                     private float hangCounter;

                     private bool faceRight = true;
                     private bool isOnGround;
                     private bool isUnderGround;

    [Header("Kick")]
    [SerializeField] private Transform kickPoint;
    [SerializeField] private int kickDamage;
    [SerializeField] private float kickRadius;
    [SerializeField] private LayerMask kickableLayers;
                     private bool isKicking;
            
    [Header("Potions")]
    [SerializeField] private Transform castPoint;
                     private DateTime lastCastTime;
                     private PotionBag potionBag;

    [Header("Sounds")]
    public AudioSource StepSound1;
    public AudioSource StepSound2;
    public AudioSource StepSound3;
    public AudioSource ThrowSound;
    private float nextPlay = 0.0f;
    private int randomSound;
    private float walkSpeed = 0.36f;


    public Vector3 startPosition;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        potionBag = new PotionBag();
        startPosition = transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(bottomCollider.size.x * 0.98f, 0f));
        Gizmos.DrawWireCube(ceilingCheck.position, new Vector3(topCollider.size.x*0.9f, 0f));
        Gizmos.DrawWireSphere(kickPoint.position, kickRadius);
    }

    #region Moving
    public void Move(float move, bool isCrawling, bool isJumping)
    {
        //direction
        if ((move > 0 && !faceRight) || (move < 0 && faceRight))
        {
            faceRight = !faceRight;
            transform.Rotate(0, 180, 0);
        }

        //animation
        playerAnimator.SetFloat("speed", Math.Abs(move));
        playerAnimator.SetFloat("vertSpeed", playerRB.velocity.y);
        playerAnimator.SetBool("isOnGround", isOnGround);

        //movement
        playerRB.velocity = new Vector2(speed * move, playerRB.velocity.y);

        //crawling
        isUnderGround = Physics2D.OverlapBox(ceilingCheck.position, new Vector2(topCollider.size.x * 0.9f, 0f), 1, groundLayer);
        if (isUnderGround)
        {
            isCrawling = true;
        }
        topCollider.enabled = !isCrawling;

        //hang time
        if (isOnGround)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        //jumping
        isOnGround = Physics2D.OverlapBox(groundCheck.position, new Vector2(bottomCollider.size.x * 0.98f, 0f), 1, groundLayer);
        if (isCrawling)
        {
            isJumping = false;
        }
        if (isJumping && hangCounter > 0f)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
            playerRB.AddForce(Vector2.up * +jumpForce);
            //sound
            StepSound1.pitch = 1.15f;
            StepSound1.Play();
        }

        //sound
        if (isOnGround && (move != 0))
        {
            if (Time.time > nextPlay)
            {
                randomSound = UnityEngine.Random.Range(0, 4);
                switch (randomSound)
                {
                    case 1:
                        nextPlay = Time.time + walkSpeed;
                        StepSound1.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                        StepSound1.volume = UnityEngine.Random.Range(0.25f, 0.28f);
                        StepSound1.Play();
                        break;
                    case 2:
                        nextPlay = Time.time + walkSpeed;
                        StepSound2.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                        StepSound2.volume = UnityEngine.Random.Range(0.19f, 0.21f);
                        StepSound2.Play();
                        break;
                    case 3:
                        nextPlay = Time.time + walkSpeed;
                        StepSound3.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                        StepSound3.volume = UnityEngine.Random.Range(0.21f, 0.24f);
                        StepSound3.Play();
                        break;
                }
                
            }
        }
    }
    #endregion

    #region Combat
    public void StartThrowing()
    {
        if (potionBag.CurrentPotion != null)
        {
            if ((DateTime.Now - lastCastTime).TotalMilliseconds > potionBag.CurrentPotion.GetComponent<PotionStats>().CoolDown)
            {
                lastCastTime = DateTime.Now;
                Throw();

            }
        }

    }

    private void Throw()
    {   
        GameObject currentPotion = Instantiate(potionBag.CurrentPotion, castPoint.position, Quaternion.identity);
        currentPotion.GetComponent<Rigidbody2D>().velocity = castPoint.transform.right * 15f;
        //sound
        ThrowSound.pitch = UnityEngine.Random.Range(1.0f, 1.15f);
        ThrowSound.Play();
    }

    public void StartKicking()
    {
        if (isKicking) { return; }
        isKicking = true;
        Kick();
    }

    private void Kick()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(kickPoint.position, kickRadius, kickableLayers);
        for(int i = 0; i < targets.Length; i++)
        {
            
            targets[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(600 + (transform.rotation.y * 1200), 800));            
        }
        isKicking = false;
    }
    #endregion

    public void Take(GameObject item)
    {
        if(item.GetComponent<PotionStats>() != null)
        {
            potionBag.AddPotion(item);
        }
    }

    void Update()
    {
    }
}
