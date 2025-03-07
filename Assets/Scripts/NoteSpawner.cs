using UnityEngine;
using System.Collections.Generic;

public class NoteSpawner : MonoBehaviour
{
    public GameObject greenNotePrefab;
    public GameObject blueNotePrefab;
    public GameObject redNotePrefab;
    public GameObject pinkNotePrefab;

    private Vector2 greenPos = new Vector2(-4, -6);
    private Vector2 bluePos = new Vector2(-6, -6);
    private Vector2 redPos = new Vector2(-2, -6);
    private Vector2 pinkPos = new Vector2(-8, -6);

    public int totalNotes = 200; // Aumentei para manter um fluxo constante
    private int notesSpawned = 0;
    private bool isSpawning = false;

    private int currentSegment = 0;
    private float nextSpawnTime = 0f;

    // Maior duração + spawn mais intenso
    private List<(float duration, float spawnRate)> difficultySegments = new List<(float, float)>
    {
        (10f, 0.5f),
        (10f, 0.4f),
        (10f, 0.35f),
        (10f, 0.25f),
        (10f, 0.3f),
        (10f, 0.2f),
        (10f, 0.3f),
        (10f, 0.5f)
    };

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            nextSpawnTime = Time.time;
            StartCoroutine(SpawnNotes());
        }
    }

    private System.Collections.IEnumerator SpawnNotes()
    {
        while (notesSpawned < totalNotes)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnNote();

                if (currentSegment < difficultySegments.Count)
                {
                    nextSpawnTime = Time.time + difficultySegments[currentSegment].spawnRate;
                }

                notesSpawned++;
            }

            yield return null;
        }
    }

    private void SpawnNote()
    {
        int randomNote = Random.Range(0, 4);
        Vector2 spawnPos = GetNotePosition(randomNote);

        Instantiate(GetPrefab(randomNote), spawnPos, Quaternion.identity);
        GameManager.Instance.NoteSpawned();
    }

    private GameObject GetPrefab(int noteIndex)
    {
        switch (noteIndex)
        {
            case 0: return greenNotePrefab;
            case 1: return blueNotePrefab;
            case 2: return redNotePrefab;
            case 3: return pinkNotePrefab;
            default: return null;
        }
    }

    private Vector2 GetNotePosition(int noteIndex)
    {
        switch (noteIndex)
        {
            case 0: return greenPos;
            case 1: return bluePos;
            case 2: return redPos;
            case 3: return pinkPos;
            default: return Vector2.zero;
        }
    }

    private void Update()
    {
        if (isSpawning && currentSegment < difficultySegments.Count)
        {
            difficultySegments[currentSegment] = (difficultySegments[currentSegment].duration - Time.deltaTime, difficultySegments[currentSegment].spawnRate);
            if (difficultySegments[currentSegment].duration <= 0)
            {
                currentSegment++;
            }
        }
    }
}
