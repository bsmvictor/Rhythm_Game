using UnityEditor;
using UnityEngine;

public class KeyListener : MonoBehaviour
{
    public KeyCode key;
    public float perfectThreshold = 0.3f;
    public float greatThreshold = 0.5f;
    public float goodThreshold = 0.7f;

    public Sprite normalSprite;
    public Sprite pressedSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            spriteRenderer.sprite = pressedSprite;
            CheckHit();
        }

        if (Input.GetKeyUp(key))
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    void CheckHit()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, goodThreshold);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Note"))
            {
                float distance = Mathf.Abs(hit.transform.position.y - transform.position.y);

                if (distance <= perfectThreshold)
                {
                    Debug.Log("Perfect!");
                    GameManager.Instance.AddScore(300);
                }
                else if (distance <= greatThreshold)
                {
                    Debug.Log("Great!");
                    GameManager.Instance.AddScore(200);
                }
                else if (distance <= goodThreshold)
                {
                    Debug.Log("Good!");
                    GameManager.Instance.AddScore(100);
                }

                Destroy(hit.gameObject);
                break;
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, goodThreshold);
    }
}