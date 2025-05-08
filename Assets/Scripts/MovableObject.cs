using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private new Renderer renderer;
    public float speed;

    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
    }

    void Update()
    {
        transform.position = transform.position + speed * Time.deltaTime * new Vector3(-1,0,0);
        if (!renderer.isVisible && transform.position.x<0)
        {
            Destroy(gameObject,2.0f);
        }
    }
}
