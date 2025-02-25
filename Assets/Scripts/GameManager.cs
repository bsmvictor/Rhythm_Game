using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text scoreText;
    public GameObject endScreen; 
    public TMP_Text finalScoreText; 

    public GameObject startScreen;

    private int score = 0;
    private int notesDestroyed = 0;
    private int totalNotes = 100;
    private bool gameStarted = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        endScreen.SetActive(false); 
        startScreen.SetActive(true);
    }

    public void StartGame()
    {
        gameStarted = true;
        startScreen.SetActive(false);
        FindObjectOfType<NoteSpawner>().StartSpawning();
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

    private void ShowEndScreen()
    {
        endScreen.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}