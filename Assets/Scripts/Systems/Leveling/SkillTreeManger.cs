using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public List<Skill> prerequisites;
    public bool unlocked;
    // Other skill properties and effects can be added here
}

public class SkillTreeManager : MonoBehaviour
{
    public List<Skill> skills;

    private Dictionary<string, Skill> skillDictionary;

    private void Start()
    {
        InitializeSkillDictionary();
        UnlockStartingSkills();
    }

    private void InitializeSkillDictionary()
    {
        skillDictionary = new Dictionary<string, Skill>();
        foreach (Skill skill in skills)
        {
            skillDictionary.Add(skill.skillName, skill);
        }
    }

    private void UnlockStartingSkills()
    {
        foreach (Skill skill in skills)
        {
            if (skill.prerequisites.Count == 0)
            {
                skill.unlocked = true;
            }
        }
    }

    public bool CanUnlockSkill(string skillName)
    {
        if (!skillDictionary.ContainsKey(skillName))
        {
            Debug.LogWarning("Skill not found: " + skillName);
            return false;
        }

        Skill skill = skillDictionary[skillName];

        if (skill.unlocked)
        {
            Debug.LogWarning("Skill already unlocked: " + skillName);
            return false;
        }

        foreach (Skill prerequisite in skill.prerequisites)
        {
            if (!prerequisite.unlocked)
            {
                Debug.LogWarning("Prerequisite skill not unlocked: " + prerequisite.skillName);
                return false;
            }
        }

        return true;
    }

    public void UnlockSkill(string skillName)
    {
        if (!skillDictionary.ContainsKey(skillName))
        {
            Debug.LogWarning("Skill not found: " + skillName);
            return;
        }

        Skill skill = skillDictionary[skillName];

        if (skill.unlocked)
        {
            Debug.LogWarning("Skill already unlocked: " + skillName);
            return;
        }

        if (!CanUnlockSkill(skillName))
        {
            Debug.LogWarning("Cannot unlock skill: " + skillName);
            return;
        }

        skill.unlocked = true;

        // Apply any skill effects or logic here
        Debug.Log("Skill unlocked: " + skillName);
    }
}