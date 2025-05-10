using UnityEngine;
using UnityEngine.Events;

public class BasicEnemy : MovableObject
{
    public UnityEvent KillPlayer;

    void Start()
    {
        KillPlayer.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().KillPlayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KillPlayer.Invoke();
        }
    }
}
