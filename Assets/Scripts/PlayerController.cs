using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameData d;
    
    public Rigidbody2D rigidBody;
    public Collider2D collider;

    public Transform groundCheckHB;
    private Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    private ParticleSystem dust;
    private AudioManager audioManager;

    private Animator animator;
    public UnityEvent AttackDynamite;


    void Start()
    {
        InvokeRepeating(nameof(AttackListener), 0.0f, 1.0f);
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();

        dust = GetComponentInChildren<ParticleSystem>();
        dust.Play();
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Time.timeScale != 0.0f)
        {
            //Input control
            if (Input.GetKey(KeyCode.W) && IsGrounded() && rigidBody.linearVelocity.magnitude < 0.1f)
            {
                Jump();
            }
            if (Input.GetKey(KeyCode.S) && transform.position.y > -3.5f && rigidBody.linearVelocity.magnitude < 0.1f)
            {
                Drop();
            }
            if (Input.GetKey(KeyCode.D) && rigidBody.linearVelocity == Vector2.zero && !animator.GetCurrentAnimatorStateInfo(0).IsName("squirrelAttack"))
            {
                StartCoroutine(Attack(0.3f));
                
            }
            //Dust particles control
            if (Mathf.Abs(rigidBody.linearVelocity.y) < 0.1f && dust.isStopped)
            {
                dust.Play();
            }
            else if(Mathf.Abs(rigidBody.linearVelocity.y) > 0.1f)
            {
                dust.Stop();
            }
        }
    }

    void Jump()
    {
        rigidBody.linearVelocity = new Vector3(0,d.jumpForce,0);
        audioManager.playEffect(audioManager.jump);
    }

    void Drop()
    {
        StartCoroutine(DisableCollider(0.7f));
    }

    private bool IsGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckHB.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }

    private IEnumerator DisableCollider(float timer)
    {
        collider.isTrigger=true;
        yield return new WaitForSeconds(timer);
        collider.isTrigger = false;
    }

    private IEnumerator Attack(float timer)
    {
        animator.Play("squirrelAttack");
        yield return new WaitForSeconds(timer);
        for (int i = 0; i < 8; i++)
        {
            var hit = Physics2D.BoxCast(transform.position + Vector3.right*2f, new Vector2(1,2), 0.0f, transform.right, 1, LayerMask.GetMask("Hittable"));
            if (hit && hit.collider.tag.Equals("Dynamite"))
            {
                AttackDynamite.Invoke();
            }
            yield return new WaitForSeconds(0.0167f);
        }
    }

    void AttackListener()
    {
        try { 
            AttackDynamite.AddListener(GameObject.FindGameObjectWithTag("Dynamite").GetComponent<Dynamite>().GetHit);
        }
        catch (Exception) { }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckHB.position, groundCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position+Vector3.right*2f, new Vector2(1,2));
    }
}
