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
       
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                HealthSystem health = other.gameObject.GetComponent<HealthSystem>();
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