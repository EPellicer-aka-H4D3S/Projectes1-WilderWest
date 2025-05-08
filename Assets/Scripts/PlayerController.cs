using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.1f;
    private string temp;
    public Rigidbody2D rigidBody;
    public BoxCollider2D boxCollider;
    public CircleCollider2D circleCollider;

    public Transform groundCheckHB;
    private Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();

    }

    void Update()
    {
        temp = Physics2D.OverlapCircle(transform.position, circleCollider.radius).gameObject.name;
        if (temp == "Bison(Clone)")
        {
            //functional
        }else if (temp == "Nut" || temp == "Nut (1)" || temp == "Nut (2)")
        {
            //functional
        }

        if (Input.GetKey(KeyCode.W) && IsGrounded() && rigidBody.linearVelocity == new Vector2(0, 0))
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
