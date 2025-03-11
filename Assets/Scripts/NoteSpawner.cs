using UnityEngine;
using System.Collections.Generic;

public class NoteSpawner : MonoBehaviour
{
    public GameObject greenNotePrefab;
    public GameObject blueNotePrefab;
    public GameObject redNotePrefab;
    public GameObject pinkNotePrefab;

    private Vector3 greenPos = new Vector3(-4, -6, -1);
    private Vector3 bluePos = new Vector3(-6, -6, -1);
    private Vector3 redPos = new Vector3(-2, -6, -1);
    private Vector3 pinkPos = new Vector3(-8, -6, -1);

    public int totalNotes = 200; // Aumentei para manter um fluxo constante
    private int notesSpawned = 0;
    private bool isSpawning = false;

    private int currentSegment = 0;
    private float nextSpawnTime = 0f;
    
    private float spawnDelay = 0.05f;

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
                    nextSpawnTime = Time.time + difficultySegments[currentSegment].spawnRate + spawnDelay; 
                }

                notesSpawned++;
            }

            yield return null;
        }
    }

    private void SpawnNote()
    {
        int randomNote = Random.Range(0, 4);
        Vector3 spawnPos = GetNotePosition(randomNote);

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

    private Vector3 GetNotePosition(int noteIndex)
    {
        switch (noteIndex)
        {
            case 0: return greenPos;
            case 1: return bluePos;
            case 2: return redPos;
            case 3: return pinkPos;
            default: return Vector3.zero;
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
