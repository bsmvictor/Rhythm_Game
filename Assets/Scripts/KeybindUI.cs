using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeybindUI : MonoBehaviour
{
    public Button keybindButton;
    public TextMeshProUGUI buttonText;
    public string actionName;

    private bool waitingForKey = false;

    private void Start()
    {
        // Define teclas padrão (setas direcionais)
        KeyCode savedKey = KeybindManager.Instance.LoadKey(actionName, KeyCode.None);
        buttonText.text = FormatKeyText(savedKey);

        keybindButton.onClick.AddListener(() => StartKeyRebind());
    }

    private void Update()
    {
        if (waitingForKey)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    KeybindManager.Instance.SaveKey(actionName, key);
                    buttonText.text = FormatKeyText(key);
                    waitingForKey = false;
                    break;
                }
            }
        }
    }

    private void StartKeyRebind()
    {
        buttonText.text = "Pressione uma tecla...";
        waitingForKey = true;
    }

    private string FormatKeyText(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.UpArrow: return "↑";
            case KeyCode.DownArrow: return "↓";
            case KeyCode.LeftArrow: return "←";
            case KeyCode.RightArrow: return "→";
            default: return key.ToString();
        }
    }
}