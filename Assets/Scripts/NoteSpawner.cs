using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject greenNotePrefab;
    public GameObject blueNotePrefab;
    public GameObject redNotePrefab;
    public GameObject pinkNotePrefab;

    public float spawnInterval = 0.5f;
    public int totalNotes = 100;

    private int notesSpawned = 0;

    private Vector2 greenPos = new Vector2(-4, -6);
    private Vector2 bluePos = new Vector2(-6, -6);
    private Vector2 redPos = new Vector2(-2, -6);
    private Vector2 pinkPos = new Vector2(-8, -6);

    private bool isSpawning = false;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            InvokeRepeating("SpawnNotes", 1.0f, spawnInterval);
        }
    }

    void SpawnNotes()
    {
        if (notesSpawned >= totalNotes)
        {
            CancelInvoke("SpawnNotes");
            return;
        }

        int randomNote = Random.Range(0, 4);

        switch (randomNote)
        {
            case 0:
                Instantiate(greenNotePrefab, greenPos, Quaternion.identity);
                break;
            case 1:
                Instantiate(blueNotePrefab, bluePos, Quaternion.identity);
                break;
            case 2:
                Instantiate(redNotePrefab, redPos, Quaternion.identity);
                break;
            case 3:
                Instantiate(pinkNotePrefab, pinkPos, Quaternion.identity);
                break;
        }

        notesSpawned++;
        GameManager.Instance.NoteSpawned();
    }
}