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
        if (!gameObject.GetComponentInChildren<Renderer>().isVisible && transform.position.x<0)
        {
            Destroy(gameObject,2.0f);
        }
    }
}
