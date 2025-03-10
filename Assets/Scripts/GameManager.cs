using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text scoreText;
    public TMP_Text finalScoreText;
    public TMP_Text multiplierText;

    public GameObject endScreen;
    public GameObject pauseScreen;

    public GameObject musicFloor;

    private int score = 0;
    private int notesDestroyed = 0;
    private int totalNotes = 100;
    private int totalNotesSpawned = 0;

    private bool checkingForEnd = false;
    private bool isPaused = false;

    private ComboManager comboManager;

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
        musicFloor.SetActive(true);
        Time.timeScale = 1;

        comboManager = FindObjectOfType<ComboManager>();

        if (comboManager == null)
        {
            Debug.LogError("ERRO: ComboManager não encontrado!");
        }

        NoteSpawner spawner = FindObjectOfType<NoteSpawner>();

        if (spawner == null)
        {
            Debug.LogError("ERRO: NoteSpawner não encontrado na cena!");
            return;
        }

        spawner.StartSpawning();
        UpdateMultiplierText(); // Atualiza o multiplicador ao iniciar o jogo
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !endScreen.activeSelf)
        {
            TogglePause();
        }

        UpdateMultiplierText(); // Atualiza o multiplicador em tempo real
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        musicFloor.SetActive(false);

        if (isPaused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            musicFloor.SetActive(true);
        }
    }

    public void AddScore(int baseValue)
    {
        if (comboManager == null) return;

        int multiplier = comboManager.GetMultiplier();
        int finalScore = baseValue * multiplier;

        score += finalScore;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void UpdateMultiplierText()
    {
        if (multiplierText == null || comboManager == null) return;

        int multiplier = comboManager.GetMultiplier();
        multiplierText.text = $"x{multiplier}"; // Exibe "x1", "x2", etc.
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
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
