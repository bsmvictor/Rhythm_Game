using UnityEngine;

public class DanceFloorSync : MonoBehaviour
{
    public AudioSource music;
    private Renderer tileRenderer;
    private Color[] colors = {  Color.magenta, Color.cyan };

    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float[] spectrum = new float[64];
        music.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        float intensity = spectrum[Random.Range(0, 64)];

        if (intensity > 0.1f)
        {
            tileRenderer.material.color = colors[Random.Range(0, colors.Length)];
        }
    }
}
