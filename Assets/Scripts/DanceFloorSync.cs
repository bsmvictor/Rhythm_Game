using UnityEngine;

public class DanceFloorSync : MonoBehaviour
{
    public AudioSource music;
    private SpriteRenderer tileRenderer;
    private Color[] colors = {  Color.magenta, Color.cyan };

    void Start()
    {
        tileRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float[] spectrum = new float[64];
        music.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        float intensity = spectrum[Random.Range(5, 20)];

        if (intensity > 0.01f)
        {
            tileRenderer.color = colors[Random.Range(0, colors.Length)];
        }
    }
}
