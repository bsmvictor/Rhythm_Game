using System;
using Unity.VisualScripting;
using UnityEngine;

public class FallingKey : MonoBehaviour
{
    public float beatTempo;
    public float passThreshold = 3f;
    private ComboManager comboManager;

    private void Start()
    {
        comboManager = FindObjectOfType<ComboManager>();
        beatTempo = beatTempo / 60f;
    }

    void Update()
    {
        transform.position += new Vector3(0f, beatTempo * Time.deltaTime, 0f);

        if (transform.position.y > passThreshold)
        {
            GameManager.Instance.NoteDestroyed();
            Destroy(gameObject);
            comboManager.MissNote();
        }
    }

}
