using UnityEngine;

public class MovableObject : MonoBehaviour
{
   
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = transform.position + speed * Time.deltaTime * new Vector3(-1,0,0);
        if (!gameObject.GetComponentInChildren<Renderer>().isVisible && transform.position.x<0)
        {
            Destroy(gameObject,2.0f);
        }
    }
}
