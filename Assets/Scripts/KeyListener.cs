using UnityEngine;

public class KeyListener : MonoBehaviour
{
    public string keyIdentifier; // Nome da chave usada no PlayerPrefs
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
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        comboManager = FindObjectOfType<ComboManager>();

        if (string.IsNullOrEmpty(keyIdentifier))
        {
            Debug.LogError($"[ERRO] KeyIdentifier não foi definido em {gameObject.name}");
            return;
        }
        
        if (PlayerPrefs.HasKey(keyIdentifier))
        {
            string savedKey = PlayerPrefs.GetString(keyIdentifier);
        
            if (System.Enum.TryParse(savedKey, out KeyCode parsedKey))
            {
                key = parsedKey;
                Debug.Log($"[CARREGADO] {gameObject.name} ({keyIdentifier}) = {key}");
            }
            else
            {
                Debug.LogError($"[ERRO] Tecla inválida para {gameObject.name} ({keyIdentifier}): {savedKey}");
            }
        }
        else
        {
            Debug.LogError($"[ERRO] Tecla não encontrada para {gameObject.name} ({keyIdentifier}). Usando a padrão: {key}");
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
            Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y, -1);

            if (closestDistance > goodThreshold)
            {
                Instantiate(hiteffect, effectPosition, hiteffect.transform.rotation);
                GameManager.Instance.AddScore(10);
                comboManager.IncrementCombo();
                PlaySound(HitSound);
            }
            else if (closestDistance > perfectThreshold)
            {
                Instantiate(goodeffect, effectPosition, goodeffect.transform.rotation);
                GameManager.Instance.AddScore(50);
                comboManager.IncrementCombo();
                PlaySound(HitSound);
            }
            else
            {
                Instantiate(perfecteffect, effectPosition, perfecteffect.transform.rotation);
                GameManager.Instance.AddScore(100);
                comboManager.IncrementCombo();
                PlaySound(HitSound);
            }

            GameManager.Instance.NoteDestroyed();
            Destroy(closestNote);
        }
        else
        {
            Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y, -1);
            Instantiate(misseffect, effectPosition, misseffect.transform.rotation);
            PlaySound(MissSound);
            comboManager.MissNote();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
