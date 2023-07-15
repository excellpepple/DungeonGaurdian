/*
 * Author: Excell Pepple
 * Date: 07/07/2023
 */
using System;
using System.Collections;
using System.Collections.Generic;
using Systems.Health;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthbarsprite;
    [SerializeField] private float reduceSpeed = 2;
    private float target = 1;
    [SerializeField] private Health playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    public void UpdateHealthBar(float currentHealth)
    {
        /*
         * This function updates the health bar of the player using the HUD
         * @param currentHealth the current health of the player
         * @return null
         */
        target = currentHealth / playerHealth.GetMaxHealth();
    }

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    // Update is called once per frame
    void Update()
    {
        healthbarsprite.fillAmount =
            Mathf.MoveTowards(healthbarsprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}
