using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bandit : MonoBehaviour
{
    public UnityEvent DeadBandit;

    public GameObject dynamite;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        DeadBandit.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Accelerate);
        StartCoroutine(Move());
        InvokeRepeating(nameof(SpawnDynamite),2.0f,6.0f);

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void SpawnDynamite()
    {
        Instantiate(dynamite, new Vector3(7.5f, transform.position.y - 0.5f, 0), Quaternion.identity);
    }

    public void Die()
    {
        DeadBandit.Invoke();
        rb.Sleep();
        animator.Play("dynamiteExplosion");
        Destroy(gameObject,0.5f);
    }

    private IEnumerator Move()
    {
        while (transform.position.x > 12f)
        {
            transform.position = transform.position + 2.0f * Time.deltaTime * new Vector3(-1, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        
    }
}
