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

    // Update is called once per frame
    void Update()
    {
        // Movimento suave para cima
        transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));

        // Fade-out suave
        if (spriteRenderer != null)
        {
            float alpha = Mathf.Lerp(spriteRenderer.color.a, 0, fadeSpeed * Time.deltaTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }

        // Destroi após o tempo de vida
        Destroy(gameObject, lifetime);
    }
}