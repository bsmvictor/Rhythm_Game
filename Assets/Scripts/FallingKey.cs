using UnityEngine;

public class FallingKey : MonoBehaviour
{
    public float fallSpeed = 2.0f;
    private float passThreshold = -5.0f; 

    void Update()
    {
        transform.Translate(Vector2.down * (fallSpeed * Time.deltaTime));

        if (transform.position.y < passThreshold)
        {
            Destroy(gameObject);
        }
    }
}
