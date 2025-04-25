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
    }

    public void TakeDamage(float damage)
    {
        if (!isAlive) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Que no pase de 0
        onHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (!isAlive) return;
        Debug.LogError($"No tiene sentido que se cure si esta muerto, se cura {amount}");
        Debug.LogWarning($"Se esta acabando la memoria cache");
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
        Debug.Log($"Valor de variable isAlive: {isAlive}");
        Debug.LogError($"Error de variable isAlive: {isAlive}");
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
}