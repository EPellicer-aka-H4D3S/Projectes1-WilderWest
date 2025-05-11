using UnityEngine;
using UnityEngine.Events;

public class Nut : MovableObject
{
    public UnityEvent NutColected;

    AudioManager audioManager;

    void Start()
    {
        NutColected.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().UpdateScore);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NutColected.Invoke();
            audioManager.playEffect(audioManager.nut);
            Destroy(gameObject);
        }
    }
}
