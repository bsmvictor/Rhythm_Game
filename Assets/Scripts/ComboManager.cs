using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private int comboCount = 0;
    private int currentComboLevel = 0;
    private Animator oAnimator;

    private void Start()
    {
        oAnimator = GetComponent<Animator>();
    }

    public void IncrementCombo()
    {
        comboCount++;
        CheckCombo();
    }

    public void MissNote()
    {
        ResetCombo();
    }

    private void CheckCombo()
    {
        if (comboCount >= 100)
        {
            currentComboLevel = 3;
        }
        else if (comboCount >= 50)
        {
            currentComboLevel = 2;
        }
        else if (comboCount >= 25)
        {
            currentComboLevel = 1;
        }
        else
        {
            currentComboLevel = 0;
        }

        ActivateCombo(currentComboLevel);
    }

    private void ActivateCombo(int comboLevel)
    {
        switch (comboLevel)
        {
            case 0:
                int startIndex = Random.Range(3, 5);
                oAnimator.Play($"danca{startIndex}");
                break;

            case 1:
                oAnimator.Play("danca1");
                break;

            case 2:
                oAnimator.Play("danca2");
                break;

            case 3:
                oAnimator.Play("danca5");
                break;
        }
    }

    private void ResetCombo()
    {
        if (comboCount > 0)
        {
            comboCount = 0;
            currentComboLevel = 0;

            int erroIndex = Random.Range(1, 3);
            oAnimator.Play($"erro{erroIndex}");
        }
    }
}