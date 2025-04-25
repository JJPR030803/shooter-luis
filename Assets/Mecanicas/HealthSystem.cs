using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Settings de Vida")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Events")]
    public UnityEvent onDeath;
    public UnityEvent<float> onHealthChanged;

    private bool isAlive = true;

    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"Salud al iniciar: {currentHealth}");
    }

    public void TakeDamage(float damage)
    {
        if (!isAlive) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Que no pase de 0
        onHealthChanged?.Invoke(currentHealth);
        Debug.Log($"Salud actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (!isAlive) return;
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // vida maxima
        onHealthChanged?.Invoke(currentHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth);
    }

    public void Revivir()
    {
        isAlive = true;
    }

    private void Die()
    {
        isAlive = false;
        onDeath?.Invoke();
    }

    //Metodos ayudantes
    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}