using UnityEngine;

public class FallingKey : MonoBehaviour
{
    public float fallSpeed = 2.0f;
    private float passThreshold = 1.4f; 

    void Update()
    {
        transform.Translate(Vector2.up * (fallSpeed * Time.deltaTime));

        if (transform.position.y > passThreshold)
        {
            Destroy(gameObject);
        }
    }
}
