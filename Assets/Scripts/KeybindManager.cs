using UnityEngine;

public class KeybindManager : MonoBehaviour
{
    public static KeybindManager Instance;

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

    public void SaveKey(string action, KeyCode key)
    {
        PlayerPrefs.SetString(action, key.ToString());
        PlayerPrefs.Save();
        Debug.Log($"Tecla {action} salva como: {key}");
    }

    public KeyCode LoadKey(string action, KeyCode defaultKey)
    {
        string savedKey = PlayerPrefs.GetString(action, defaultKey.ToString());
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), savedKey);
    }
}