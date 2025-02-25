using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public float lifetime = 1f;
    public float moveSpeed = 1f;
    public float fadeSpeed = 2f;

    private SpriteRenderer spriteRenderer;
    private Color startColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            startColor = spriteRenderer.color;
        }
    }
    void Update()
    {
        transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));
        
        if (spriteRenderer != null)
        {
            float alpha = Mathf.Lerp(spriteRenderer.color.a, 0, fadeSpeed * Time.deltaTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }
        
        Destroy(gameObject, lifetime);
    }
}