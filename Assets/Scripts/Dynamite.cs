using System;
using UnityEngine;
using UnityEngine.Events;

public class Dynamite : BasicEnemy
{
    public UnityEvent KillBandit;

    private AudioManager audioManager;
    private Animator animator;
    private bool hitted = false;
    private bool active = true;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        hurtBox = GetComponent<CircleCollider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Awake()
    {
        Invoke(nameof(AddListeners),0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KillPlayer.Invoke();
            hurtBox.enabled = false;
            active = false;
            animator.Play("dynamiteExplosion");
            audioManager.playEffect(audioManager.explosion);
            Destroy(gameObject,0.5f);
        }
        else if (collision.CompareTag("Bandit"))
        {
            KillBandit.Invoke();
            active = false;
            animator.Play("dynamiteExplosion");
            audioManager.playEffect(audioManager.explosion);
            Destroy(gameObject,0.5f);
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
        if (active)
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
}
