using UnityEngine;

public class CriarNotaLonga : MonoBehaviour
{
    public GameObject topo;
    public GameObject meio;
    public GameObject final;

    public float tempo;
    public int tempoNota;
    
    private void Start()
    {
        Instantiate(topo, transform.position, Quaternion.identity);
        Instantiate(final, transform.position, Quaternion.identity);
    }
    
    private void Update()
    {
        for (int i = 0; i < tempoNota; i++)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + i, transform.position.z);
            Instantiate(meio, position, Quaternion.identity);
        }
    }
}
