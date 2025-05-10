using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bandit : MonoBehaviour
{
    public UnityEvent DeadBandit;

    public GameObject dynamite;

    void Start()
    {
        DeadBandit.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Accelerate);
        StartCoroutine(Move());
        InvokeRepeating(nameof(SpawnDynamite),2.0f,6.0f);
    }

    void SpawnDynamite()
    {
        Instantiate(dynamite, new Vector3(7.5f, transform.position.y, 0), Quaternion.identity);
    }

    public void Die()
    {
        DeadBandit.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator Move()
    {
        while (transform.position.x > 12f)
        {
            transform.position = transform.position + 2.0f * Time.deltaTime * new Vector3(-1, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        
    }

    void Update()
    {

    }
}
