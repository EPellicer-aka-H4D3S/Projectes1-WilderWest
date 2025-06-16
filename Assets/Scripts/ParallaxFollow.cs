using UnityEngine;

public class ParallaxFollow : MonoBehaviour
{
    public GameData d;

    void Update()
    {
        transform.position = transform.position + d.speed * Time.deltaTime * Vector3.left;
    }
}
