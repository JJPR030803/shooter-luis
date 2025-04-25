using System;
using UnityEngine;

namespace Jugador
{
    public class Hitbox : MonoBehaviour
    {
        [Header("Hitbox Settings")]
        [SerializeField] private float damage = 10f;
        [SerializeField] private bool destruir = false;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Colisiona con el jugador");
                HealthSystem health = other.gameObject.GetComponent<HealthSystem>();
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