using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.1f;
    public Rigidbody2D rigidBody;
    public CircleCollider2D circleCollider;

    public Transform groundCheckHB;
    private Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
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
            //Attack
        }
    }

    void Jump()
    {
        rigidBody.linearVelocity = new Vector3(0,speed,0);
    }

    void Drop()
    {
        StartCoroutine(DisableCollider(1f));
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckHB.position, groundCheckSize);
    }
}
