using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image healthbar;
    public Player player;

    public float lerpSpeed;

    // Update is called once per frame
    void Update()
    {
        if (player.currentHealth > player.maxHealth)
        {
            player.currentHealth = player.maxHealth;
        }
        if (player.currentHealth < player.minHealth)
        {
            player.currentHealth = player.minHealth;
        }
        lerpSpeed = 6f * Time.deltaTime;

        HealthBarFiller();
    }

    private void HealthBarFiller()
    {
        healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, player.currentHealth / player.maxHealth, lerpSpeed);
        Color healthColor = Color.HSVToRGB(0, 0.72f, player.currentHealth / player.maxHealth + 0.4f);
        healthbar.color = healthColor;
    }

    public void Damage(int damagePoints)
    {
        if (player.currentHealth > 0)
        {
            player.currentHealth -= damagePoints;
        }
    }

    public void Heal(int healingPoints)
    {
        if (player.currentHealth < player.maxHealth)
        {
            player.currentHealth += healingPoints;
        }
    }
}
