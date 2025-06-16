using UnityEngine;
using UnityEngine.Events;

public class Dynamite : MovableObject
{
    public UnityEvent KillPlayer;
    public UnityEvent KillBandit;
    
    private Animator animator;
    private bool hitted = false;
    

    void Start()
    {
        InvokeRepeating(nameof(AddListeners), 0.0f, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KillPlayer.Invoke();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Bandit"))
        {
            KillBandit.Invoke();
            Destroy(gameObject);
        }
    }

    public void GetHit()
    {
        hitted = true;
    }

    void AddListeners()
    {
        KillPlayer.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().KillPlayer);
        KillBandit.AddListener(GameObject.FindGameObjectWithTag("Bandit").GetComponent<Bandit>().Die);
    }

    void Update()
    {
        if (hitted)
        {
            transform.position += (d.speed + addedSpeed) * 2 * Time.deltaTime * Vector3.right;
            if (Time.timeScale != 0)
            {
                transform.Rotate(Vector3.forward * 2);
            }
        }
        else
        {
            transform.position += (d.speed + addedSpeed) * Time.deltaTime * Vector3.left;
            if (Time.timeScale != 0)
            {
                transform.Rotate(Vector3.forward);
            }
        }

        if (transform.position.x < -20.0f || transform.position.x > 20.0f)
        {
            Destroy(gameObject);
        }
    }
}
