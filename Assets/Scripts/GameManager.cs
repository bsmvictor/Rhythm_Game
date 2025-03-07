using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text scoreText;
    public TMP_Text finalScoreText;

    public GameObject endScreen;
    public GameObject pauseScreen;

    private int score = 0;
    private int notesDestroyed = 0;
    private int totalNotes = 100;
    private int totalNotesSpawned = 0;

    private bool checkingForEnd = false;
    private bool isPaused = false;

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
        endScreen.SetActive(false);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;

        NoteSpawner spawner = FindObjectOfType<NoteSpawner>();

        if (spawner == null)
        {
            Debug.LogError("ERRO: NoteSpawner não encontrado na cena!");
            return;
        }

        spawner.StartSpawning();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !endScreen.activeSelf)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pausa o jogo
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1; // Retoma o jogo
            pauseScreen.SetActive(false);
        }
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

    public void NoteSpawned()
    {
        totalNotesSpawned++;
    }

    public void NoteDestroyed()
    {
        notesDestroyed++;

        if (notesDestroyed >= totalNotes)
        {
            Invoke("CheckForRemainingNotes", 1.5f);
        }
    }

    private void CheckForRemainingNotes()
    {
        if (!checkingForEnd && GameObject.FindGameObjectsWithTag("Note").Length == 0)
        {
            checkingForEnd = true;
            ShowEndScreen();
        }
    }

    public void ShowEndScreen()
    {
        Debug.Log("Fim do jogo! Exibindo tela final...");
        finalScoreText.text = "Final Score: " + score;
        endScreen.SetActive(true);
        Time.timeScale = 0; // Garante que o jogo fique pausado na tela final
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Garante que o jogo não reinicie pausado
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
