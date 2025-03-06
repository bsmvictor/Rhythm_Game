using UnityEngine;

public class DanceFloorControl : MonoBehaviour
{
    private Renderer tileRenderer;
    private Color[] colors = { Color.magenta, Color.cyan };

    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
        InvokeRepeating("ChangeColor", 0f, 0.5f); 
    }

    void ChangeColor()
    {
        tileRenderer.material.color = colors[Random.Range(0, colors.Length)];
    }
    
}
