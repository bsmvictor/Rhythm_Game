using UnityEngine;

public class DanceFloorControl : MonoBehaviour
{
    private SpriteRenderer tileRenderer;
    private Color[] colors = { Color.magenta, Color.cyan };

    void Start()
    {
        tileRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("ChangeColor", 0f, 0.5f); 
    }

    void ChangeColor()
    {
        tileRenderer.color = colors[Random.Range(0, colors.Length)];
    }
    
}
