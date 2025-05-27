using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.1f;
    public Rigidbody2D rigidBody;
    public CircleCollider2D circleCollider;

    public Transform groundCheckHB;
    private Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    AudioManager audioManager;

    public UnityEvent Attack;


    void Start()
    {
        InvokeRepeating(nameof(AttackListener), 0.0f, 1.0f);
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) && IsGrounded() && rigidBody.linearVelocity == Vector2.zero)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y>-3.5f && rigidBody.linearVelocity == Vector2.zero)
        {
            Drop();
        }
        if (Input.GetKey(KeyCode.D) && rigidBody.linearVelocity == Vector2.zero)
        {
            Attack.Invoke();
        }
    }

    void Jump()
    {
        rigidBody.linearVelocity = new Vector3(0,speed,0);
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
        circleCollider.isTrigger=true;
        yield return new WaitForSeconds(timer);
        circleCollider.isTrigger = false;
    }

    void AttackListener()
    {
        try { 
        Attack.AddListener(GameObject.FindGameObjectWithTag("Dynamite").GetComponent<Dynamite>().GetHit);
        }
        catch (Exception) { }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckHB.position, groundCheckSize);
    }
}
