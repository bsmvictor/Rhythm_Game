using UnityEngine;

public class KeyListener : MonoBehaviour
{
    public KeyCode key;
    public float perfectThreshold = 0.3f;
    public float goodThreshold = 0.5f;
    public float greatThreshold = 0.7f;

    public Sprite normalSprite;
    public Sprite pressedSprite;

    public AudioClip HitSound;
    public AudioClip MissSound;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;

    private ComboManager comboManager;

    public GameObject hiteffect, goodeffect, perfecteffect, misseffect;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, greatThreshold);

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
            if (closestNote != null)
            {
                Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y, -1);

                if (closestDistance > goodThreshold)
                {
                    //Debug.Log("Great!");
                    Instantiate(hiteffect, effectPosition, hiteffect.transform.rotation);
                    GameManager.Instance.AddScore(10);
                    comboManager.IncrementCombo();
                    PlaySound(HitSound);
                }
                else if (closestDistance > perfectThreshold)
                {
                    //Debug.Log("Good!");
                    Instantiate(goodeffect, effectPosition, goodeffect.transform.rotation);
                    GameManager.Instance.AddScore(50);
                    comboManager.IncrementCombo();
                    PlaySound(HitSound);
                }
                else
                {
                    //Debug.Log("Perfect!");
                    Instantiate(perfecteffect, effectPosition, perfecteffect.transform.rotation);
                    GameManager.Instance.AddScore(100);
                    comboManager.IncrementCombo();
                    PlaySound(HitSound);
                }

                GameManager.Instance.NoteDestroyed();
                Destroy(closestNote);
            }
        }
        else
        {
            comboManager.ResetCombo();
            Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y, -1);
            Instantiate(misseffect, effectPosition, misseffect.transform.rotation);
            PlaySound(MissSound);
            //Debug.Log("Miss!");
        }
    }


    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, perfectThreshold);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, goodThreshold);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, greatThreshold);
    }
}
