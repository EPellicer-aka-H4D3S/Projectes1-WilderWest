using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.1f;
    public Rigidbody2D rb;

    public Transform groundCheckHB;
    private Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Input.GetButton("Jump"))&&IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(0,speed,0);
    }

    private bool IsGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckHB.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckHB.position, groundCheckSize);
    }
}
