using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHealth : MonoBehaviour
{
    [SerializeField] private float health;
    public float initialHealth;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get; set; }
    private void Awake()
    {
        initialHealth = Health;
        MaxHealth = Health;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Death();
            Health = 0;
        }
    }

    public virtual void GainHealth(float extraHealth)
    {
        Health += extraHealth;
        if (Health >= MaxHealth)
        {
            Health = MaxHealth;
        }
    }
    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
