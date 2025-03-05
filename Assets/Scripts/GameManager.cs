using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text scoreText;
    public TMP_Text finalScoreText; 


    private int score = 0;
    private int notesDestroyed = 0;
    private int totalNotes = 100;
    private bool gameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        gameStarted = true;
        Debug.Log("StartGame foi chamado!"); 
        
        NoteSpawner spawner = FindObjectOfType<NoteSpawner>();

        if (spawner == null)
        {
            Debug.LogError("ERRO: NoteSpawner não encontrado na cena!");
            return;
        }

        spawner.StartSpawning();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
    
    private int totalNotesSpawned = 0;

    public void NoteSpawned()
    {
        totalNotesSpawned++;
    }

    public void NoteDestroyed()
    {
        notesDestroyed++;
        if (notesDestroyed >= totalNotes)
        {
            Invoke("ShowEndScreen", 2f);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}