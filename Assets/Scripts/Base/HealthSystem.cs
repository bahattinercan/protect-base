using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnMaxHealthChanged;

    public event EventHandler OnDamaged;

    public event EventHandler OnHealed;

    public event EventHandler OnDied;

    [SerializeField] private int healthMax = 100;
    private int health;

    public int Health { get => health; set => health = value; }
    public int HealthMax { get => healthMax; set => healthMax = value; }

    private void Awake()
    {
        Health = HealthMax;
    }

    public void Damage(int damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, HealthMax);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    internal void SetHealthMax(int healthMax, bool updateHealthAmount = true)
    {
        this.HealthMax = healthMax;
        if (updateHealthAmount)
        {
            Health = healthMax;
        }
        OnMaxHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return Health == 0;
    }

    public bool IsFullHealth()
    {
        return Health == HealthMax;
    }

    public void Heal(int healthAmount)
    {
        Health += healthAmount;
        Health = Mathf.Clamp(Health, 0, HealthMax);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        Health = HealthMax;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public float Get_HealthAmountNormalized()
    {
        return (float)Health / HealthMax;
    }
}