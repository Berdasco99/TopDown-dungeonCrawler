using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image healthbar;
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        
        HealthBarFiller();
    }

    private void HealthBarFiller()
    {
        // Calculate the percentage of current health relative to max health
        float healthPercentage = player.currentHealth / player.maxHealth;

        // Adjust lerp speed for smoother transition (experiment with values)
        float lerpSpeed = 5f * Time.deltaTime;

        // Update the health bar fill amount using lerping
        healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, healthPercentage, lerpSpeed);

        // Debug log to track the fill amount
        Debug.Log($"Health Bar Fill Amount: {healthbar.fillAmount}");
    }
}
