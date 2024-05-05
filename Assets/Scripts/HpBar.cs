using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image healthbar;

    public float health, maxHealth = 100f, minHealth = 0f;
    public float lerpSpeed;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health < minHealth)
        {
            health = minHealth;
        }
        lerpSpeed = 6f * Time.deltaTime;

        HealthBarFiller();
    }

    private void HealthBarFiller()
    {
        healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, health / maxHealth, lerpSpeed);
        Color healthColor = Color.HSVToRGB(0, 0.72f, health / maxHealth + 0.4f);
        healthbar.color = healthColor;
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
        {
            health -= damagePoints;
        }
    }

    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
        {
            health += healingPoints;
        }
    }
}
