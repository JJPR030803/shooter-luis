using UnityEngine;

public class DamageTest : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private float damageAmount = 10f;
    
    private void Start()
    {
        // If not assigned in inspector, try to get from the same GameObject
        if (healthSystem == null)
        {
            healthSystem = GetComponent<HealthSystem>();
        }
    }

    private void Update()
    {
        // Press Space to take damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthSystem.TakeDamage(damageAmount);
            Debug.Log($"Took {damageAmount} damage! Current health: {healthSystem.GetCurrentHealth()}");
        }

        // Press R to reset health
        if (Input.GetKeyDown(KeyCode.R))
        {
            healthSystem.ResetHealth();
            healthSystem.Revivir();
            Debug.Log("Health reset to maximum!");
        }

        // Press H to heal
        if (Input.GetKeyDown(KeyCode.H))
        {
            healthSystem.Heal(damageAmount);
            Debug.Log($"Healed {damageAmount} points! Current health: {healthSystem.GetCurrentHealth()}");
        }
    }
}