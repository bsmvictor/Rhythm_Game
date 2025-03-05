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
    }

    public KeyCode LoadKey(string action, KeyCode defaultKey)
    {
        if (!PlayerPrefs.HasKey(action))
        {
            // Definição das teclas padrão
            switch (action)
            {
                case "Up":
                    return KeyCode.UpArrow;
                case "Down":
                    return KeyCode.DownArrow;
                case "Left":
                    return KeyCode.LeftArrow;
                case "Right":
                    return KeyCode.RightArrow;
                default:
                    return defaultKey; // Se a ação não for encontrada, retorna o default passado
            }
        }

        string savedKey = PlayerPrefs.GetString(action, defaultKey.ToString());
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), savedKey);
    }
}