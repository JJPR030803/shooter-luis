using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
   [Header("Settings de daño")]
   [SerializeField] private float damage = 10f;
   [SerializeField] private bool destruir_al_colisionar = false;
   [SerializeField] private string[] target_tags = {"Player"};

   private void OnTriggerEnter(Collider other)
   {
      bool objetivo_es_valido = false;
      foreach (string tag in target_tags)
      {
         if (other.CompareTag(tag))
         {
            objetivo_es_valido = true;
         }
      }

      if (objetivo_es_valido)
      {
         HealthSystem health = other.GetComponent<HealthSystem>();
         if (health != null)
         {
            health.TakeDamage(damage);
            if (destruir_al_colisionar)
            {
               Destroy(gameObject);
            }
         }
      }
   }
}
