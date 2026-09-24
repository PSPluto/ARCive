using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int CurrentHealth{get; private set;}
    
    public event Action Died;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Dead");
        Died?.Invoke();
    }
}
