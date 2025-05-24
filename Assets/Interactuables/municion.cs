using UnityEngine;

public class MunicionPickup : MonoBehaviour
{
    [Header("Configuración de Munición")]
    [SerializeField] private int cantidadMunicion = 30;
    [SerializeField] private bool destruirAlRecoger = true;
    [SerializeField] private AudioClip sonidoRecoleccion; // Opcional

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            disparo sistemaDisparo = other.gameObject.GetComponent<disparo>();
            
            if (sistemaDisparo != null)
            {
                // Añadir munición al jugador
                sistemaDisparo.AñadirMunicion(cantidadMunicion);

                // Reproducir sonido si existe
                if (sonidoRecoleccion != null)
                {
                    AudioSource.PlayClipAtPoint(sonidoRecoleccion, transform.position);
                }

                // Destruir el objeto si está configurado así
                if (destruirAlRecoger)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Opcional: Hacer que la munición rote o flote
    [Header("Efectos Visuales")]
    [SerializeField] private bool rotar = true;
    [SerializeField] private float velocidadRotacion = 100f;
    [SerializeField] private bool flotar = true;
    [SerializeField] private float alturaFlotacion = 0.5f;
    [SerializeField] private float velocidadFlotacion = 1f;

    private Vector3 posicionInicial;

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        if (rotar)
        {
            transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
        }

        if (flotar)
        {
            float nuevaY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotacion) * alturaFlotacion;
            transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
        }
    }
}