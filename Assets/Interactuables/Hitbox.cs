using System;
using UnityEngine;

namespace Jugador
{
    public class Hitbox : MonoBehaviour
    {
        [Header("Hitbox Settings")]
        [SerializeField] private float damage = 10f;
        [SerializeField] private bool destruir = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                HealthSystem health = other.GetComponent<HealthSystem>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    if (destruir)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}