using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] private Image xpBarSprite;
    [SerializeField] private float reduceSpeed = 2;
    private float target = 1;
    [SerializeField] private  LevelManger _levelManger;
    public GameObject lvlText;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = lvlText.GetComponent<TextMeshProUGUI>();
        _levelManger = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelManger>();
    }

    public void UpdateXPBar(float currentLevel)
    {
        /*
         * This function updates the health bar of the player using the HUD
         * @param currentHealth the current health of the player
         * @return null
         */
        target = currentLevel / _levelManger.GetExperienceToLevelUp();
    }

    public void UpdateLvl(float currentLevel)
    {
        textMesh.text = "Level "+ _levelManger.currentLevel.ToString();
    }

    private void OnEnable()
    {
        _levelManger.OnXpChanged += UpdateXPBar;
        _levelManger.OnLevelChanged += UpdateLvl;
    }

    private void OnDisable()
    {
        _levelManger.OnXpChanged -= UpdateXPBar;
        _levelManger.OnLevelChanged += UpdateLvl;

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            _levelManger.GivePoints();
        }*/
        
        xpBarSprite.fillAmount =
            Mathf.MoveTowards(xpBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
        
    }

    
}
