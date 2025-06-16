using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public GameData d;
    public float addedSpeed;

    void Update()
    {
        transform.position += (d.speed + addedSpeed) * Time.deltaTime * Vector3.left;
        if (transform.position.x < -20.0f)
        {
            Destroy(gameObject);
        }
    }
}
