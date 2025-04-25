using UnityEngine;
using UnityEngine.UI;

public class VidaUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private HealthSystem playerHealth;

    void Start()
    {
        // Encuentra el componente de vida del jugador
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<HealthSystem>();
        }

        // Subscribir a cambios de vida
        if (playerHealth != null)
        {
            playerHealth.onHealthChanged.AddListener(UpdateHealthBar);
            
            // Vida inicial
            healthSlider.maxValue = playerHealth.GetMaxHealth();
            healthSlider.value = playerHealth.GetCurrentHealth();
        }
    }

    private void UpdateHealthBar(float newHealth)
    {
        healthSlider.value = newHealth;
    }
}