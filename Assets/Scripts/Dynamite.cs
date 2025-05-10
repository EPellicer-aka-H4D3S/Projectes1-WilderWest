using UnityEngine;
using UnityEngine.Events;

public class Dynamite : MovableObject
{
    public UnityEvent KillPlayer;
    public UnityEvent KillBandit;
    private bool hitted = false;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        if (transform.position.x < -7.5f && transform.position.y < playerController.transform.position.y+1.5f && transform.position.y > playerController.transform.position.y - 0.5f)
        {
            hitted = true;
            speed = 20;
        }
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
            transform.position = transform.position + speed * Time.deltaTime * Vector3.right;
            transform.Rotate(Vector3.forward*5);
        }
        else
        {
            transform.position = transform.position + speed * Time.deltaTime * Vector3.left;
            transform.Rotate(Vector3.forward);
        }
        if (!gameObject.GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject, 2.0f);
        }
    }
}
