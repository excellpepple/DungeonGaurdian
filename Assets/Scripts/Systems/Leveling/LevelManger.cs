using Lean.Transition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    public static LevelManger current;

    public LeanManualAnimation LevelUpEffect;


    public int currentLevel = 1;
    public int experiencePoints = 0;
    public AnimationCurve experienceCurve;

    public delegate void XpChanged(float currentLevel);
    public event XpChanged OnXpChanged;

    public delegate void LevelChanged(float currentLevel);
    public event LevelChanged OnLevelChanged;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        OnXpChanged?.Invoke(experiencePoints);
        OnLevelChanged?.Invoke(currentLevel);
    }

    public void GainExperience(int amount)
    {
        experiencePoints += amount;
        OnXpChanged?.Invoke(experiencePoints);
        if (experiencePoints >= GetExperienceToLevelUp(currentLevel))
        {
            LevelUp();
            
        }
        
    }

    private void LevelUp()
    {
        LevelUpEffect.BeginTransitions();

        currentLevel++;

        experiencePoints = 0;
        OnXpChanged?.Invoke(experiencePoints);
        OnLevelChanged?.Invoke(currentLevel);
    }

    private int GetExperienceToLevelUp(int level)
    {
        float evaluatedValue = experienceCurve.Evaluate((float)level / 10);
        Debug.Log(evaluatedValue);
        return Mathf.RoundToInt(evaluatedValue * 1000);
    }

    public int GetExperienceToLevelUp()
    {
        return GetExperienceToLevelUp(currentLevel);
    }
    
    [ContextMenu("Give XP")]
    public void GivePoints()
    {
        GainExperience(2);
        Debug.Log("XP Needed: " + GetExperienceToLevelUp(currentLevel));
        OnXpChanged?.Invoke(experiencePoints);
    }

}
