using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : GeneralHealth
{
    public System.Action OnPlayerDeath;
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateHealthUI();
        CombatTextManager.Instance.SpawnText((new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z)), damage, Color.blue);
    }
    public override void GainHealth(float extraHealth)
    {
        base.GainHealth(extraHealth);
        UpdateHealthUI();
        CombatTextManager.Instance.SpawnText((new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z)), extraHealth,
            Color.yellow);
    }
    public void ModifyMaxHealth(int modifier)
    {
        MaxHealth = initialHealth + modifier;
        UpdateHealthUI();
    }
    public void ModifyMaxHealth(float multiplier)
    {
        MaxHealth = initialHealth * multiplier;
        UpdateHealthUI();
    }
    public void UpdateHealthUI()
    {
        HealthBar.Instance.UpdateHealthBar();
        HealthCounter.Instance.UpdateHealthCounter();
    }
    public void EqualizeHealth()
    {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        UpdateHealthUI();
    }
    public override void Death()
    {
        gameObject.SetActive(false);
        OnPlayerDeath?.Invoke();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GainHealth(2);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage(1);
        }
    }
}
