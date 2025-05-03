using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.1f;
    public Rigidbody2D rigidBody;
    public BoxCollider2D boxCollider;

    public Transform groundCheckHB;
    private Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)&&IsGrounded() && rigidBody.linearVelocity == new Vector2(0, 0))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.S) && transform.position.y>-3.5f && rigidBody.linearVelocity==new Vector2(0,0))
        {
            Drop();
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
        boxCollider.isTrigger=true;
        yield return new WaitForSeconds(timer);
        boxCollider.isTrigger = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckHB.position, groundCheckSize);
    }
}
