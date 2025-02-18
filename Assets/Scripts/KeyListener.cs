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

    public AudioClip perfectHitSound;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;

    private ComboManager comboManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Verifica se já existe um AudioSource, senão adiciona um
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        comboManager = FindObjectOfType<ComboManager>();
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

        GameObject closestNote = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Note"))
            {
                float distance = Mathf.Abs(hit.transform.position.y - transform.position.y);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNote = hit.gameObject;
                }
            }
        }

        if (closestNote != null)
        {
            if (closestDistance <= perfectThreshold)
            {
                Debug.Log("Perfect!");
                GameManager.Instance.AddScore(300);
                comboManager.IncrementCombo();

                if (perfectHitSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(perfectHitSound);
                }
            }
            else if (closestDistance <= greatThreshold)
            {
                Debug.Log("Great!");
                GameManager.Instance.AddScore(200);
                comboManager.IncrementCombo();
            }
            else if (closestDistance <= goodThreshold)
            {
                Debug.Log("Good!");
                GameManager.Instance.AddScore(100);
                comboManager.IncrementCombo();
            }

            Destroy(closestNote);
        }
        else
        {
            comboManager.ResetCombo();
        }
    }
    

    private void OnDrawGizmos()
    {
        // Exibe o threshold de "Good" (vermelho)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, goodThreshold);

        // Exibe o threshold de "Great" (amarelo)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, greatThreshold);

        // Exibe o threshold de "Perfect" (verde)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, perfectThreshold);
    }
}
