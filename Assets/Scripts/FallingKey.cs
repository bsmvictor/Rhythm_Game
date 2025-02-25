using System;
using Unity.VisualScripting;
using UnityEngine;

public class FallingKey : MonoBehaviour
{
    public float beatTempo;
    private float passThreshold = 1.4f;

    private void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    void Update()
    {
        transform.position += new Vector3(0f, beatTempo * Time.deltaTime, 0f);

        if (transform.position.y > passThreshold)
        {
            Destroy(gameObject);
        }
    }
}
