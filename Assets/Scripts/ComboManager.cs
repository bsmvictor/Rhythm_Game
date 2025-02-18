using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private int comboCount = 0;
    private int currentComboLevel = 0;
    private readonly int[] comboThresholds = { 25, 50, 100 };

    public void IncrementCombo()
    {
        comboCount++;
        CheckCombo();
    }

    private void CheckCombo()
    {
        for (int i = 0; i < comboThresholds.Length; i++)
        {
            if (comboCount == comboThresholds[i] && currentComboLevel < (i + 1))
            {
                currentComboLevel = i + 1;
                ActivateCombo(currentComboLevel);
            }
        }
    }

    private void ActivateCombo(int comboLevel)
    {
        Debug.Log($"Combo {comboLevel} ativado! ({comboCount} acertos consecutivos)");

    }

    public void ResetCombo()
    {
        comboCount = 0;
        currentComboLevel = 0;
        Debug.Log("Combo resetado!");
    }
}
