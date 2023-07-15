using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private CombatController combatSystem;

    private void Start()
    {
        if (combatSystem != null)
        {
            combatSystem.onManaChange.AddListener(UpdateManaBar);
        }
    }

    private void UpdateManaBar(int currentMana, int maxMana)
    {
        if (fillImage != null)
        {
            float fillAmount = (float)currentMana / maxMana;
            fillImage.fillAmount = fillAmount;
        }
    }
}
