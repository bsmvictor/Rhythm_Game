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

    [Header("Combo Settings")]
    private int comboCount = 0;
    private int currentComboLevel = 0;
    private readonly int[] comboThresholds = {25, 50, 100};

    [Header("Animator Settings")]
    public Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Verifica se já existe um AudioSource, senão adiciona um
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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
                IncrementCombo();

                if (perfectHitSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(perfectHitSound);
                }
            }
            else if (closestDistance <= greatThreshold)
            {
                Debug.Log("Great!");
                GameManager.Instance.AddScore(200);
                IncrementCombo();
            }
            else if (closestDistance <= goodThreshold)
            {
                Debug.Log("Good!");
                GameManager.Instance.AddScore(100);
                IncrementCombo();
            }

            Destroy(closestNote);
        }
        else
        {
            ResetCombo();
        }
    }

    private void IncrementCombo()
    {
        comboCount++;
        CheckCombo();
        PlayDanceAnimation();
    }

    private void CheckCombo()
    {
        for (int i = 0; i < comboThresholds.Length; i++)
        {
            if (comboCount == comboThresholds[i] && currentComboLevel < (i + 1))
            {
                currentComboLevel = i + 1;
                ActivateCombo(currentComboLevel);
            }
        }
    }

    private void ActivateCombo(int comboLevel)
    {
        Debug.Log($"Combo {comboLevel} ativado! ({comboCount} acertos consecutivos)");
    }

    private void ResetCombo()
    {
        comboCount = 0;
        currentComboLevel = 0;
        Debug.Log("Combo resetado!");
        PlayErrorAnimation();
    }

    private void PlayDanceAnimation()
    {
        if (animator == null) return;

        if (comboCount >= 100)
        {
            animator.SetTrigger("Dance5");
        }
        else if (comboCount >= 50)
        {
            animator.SetTrigger("Dance4");
        }
        else if (comboCount >= 25)
        {
            animator.SetTrigger("Dance3");
        }
        else
        {
            int randomDance = Random.Range(1, 3);
            animator.SetTrigger("Dance" + randomDance);
        }
    }

    private void PlayErrorAnimation()
    {
        if (animator == null) return;

        int randomError = Random.Range(1, 3); 
        animator.SetTrigger("Error" + randomError);
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
