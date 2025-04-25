using System;
using UnityEngine;

namespace Jugador
{
    
    public class Botiquin : MonoBehaviour
    {
        [Header("Botiquin Settings")]
        [SerializeField]
        private float cantidadSanar = 25f;
        [SerializeField]private bool destruir = false;
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                HealthSystem health = other.GetComponent<HealthSystem>();
                if (health != null)
                {
                    health.Heal(cantidadSanar);
                    if (destruir)
                    {
                        Destroy(gameObject);
                    }
                }

                
            }
        }
    }
}