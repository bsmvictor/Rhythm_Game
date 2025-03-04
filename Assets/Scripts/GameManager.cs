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



    public void StartGame()
    {
        gameStarted = true;
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

    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}