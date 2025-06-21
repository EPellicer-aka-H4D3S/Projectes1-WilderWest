using UnityEngine;
using UnityEngine.Events;

public class BasicEnemy : MovableObject
{
    protected CircleCollider2D hurtBox;
    public UnityEvent KillPlayer;

    void Start()
    {
        hurtBox = GetComponent<CircleCollider2D>();
        KillPlayer.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().KillPlayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KillPlayer.Invoke();
            hurtBox.enabled = false;
        }
    }
}
