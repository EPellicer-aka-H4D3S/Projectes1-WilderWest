using UnityEngine;

public class MovableObject : MonoBehaviour
{
   
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = transform.position + speed * Time.deltaTime * Vector3.left;
        if (transform.position.x < -20.0f)
        {
            Destroy(gameObject);
        }
    }
}
