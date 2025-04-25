using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private HealthSystem health;

    void Start()
    {
        health = GetComponent<HealthSystem>();
        
        // Subscribe to events
        health.onDeath.AddListener(OnPlayerDeath);
        health.onHealthChanged.AddListener(OnHealthChanged);
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player has died!");
        // Logica de cuando muere
    }

    private void OnHealthChanged(float newHealth)
    {
        Debug.Log($"Health changed to: {newHealth}");

    }

    // Ejemplo de tomar daño
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(10f);
        }
    }
}