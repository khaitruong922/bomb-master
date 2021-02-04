using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health health;
    private Image healthBar;
    private void Awake()
    {
        healthBar = GetComponent<Image>();
        health = GetComponentInParent<Health>();
    }
    private void Start()
    {
        health.OnHealthChanged += UpdateHealthBar;
    }
    private void UpdateHealthBar(float changedAmount)
    {
        if (health == null) return;
        healthBar.fillAmount = health.Percentage;

    }
    private void OnDestroy()
    {
        health.OnHealthChanged -= UpdateHealthBar;
    }
}
