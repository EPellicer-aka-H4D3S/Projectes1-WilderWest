using UnityEngine;
using UnityEngine.Events;

public class Nut : MovableObject
{
    public UnityEvent NutColected;

    void Start()
    {
        NutColected.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().UpdateScore);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NutColected.Invoke();
            Destroy(gameObject);
        }
    }
}
