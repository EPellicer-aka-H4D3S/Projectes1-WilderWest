using UnityEngine;

public class ParallaxFollow : MonoBehaviour
{
   
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = transform.position + speed * Time.deltaTime * Vector3.left;
    }
}
