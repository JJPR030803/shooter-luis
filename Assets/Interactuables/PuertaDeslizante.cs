using UnityEngine;

public class PuertaDeslizante : MonoBehaviour
{
    [Header("Componentes de la Puerta")]
    public GameObject columnaIzquierda;
    public GameObject columnaDerecha;
    public GameObject puertaCentral;

    [Header("Configuración del Movimiento")]
    [SerializeField] private float velocidadMovimiento = 2f;
    [SerializeField] private float distanciaApertura = 3f;
    [SerializeField] private float tiempoEsperaAutoCierre = 2f;

    [Header("Configuración del Trigger")]
    [SerializeField] private float radioDeteccion = 3f;
    [SerializeField] private LayerMask capaJugador;

    // Variables privadas
    private Vector3 posicionCerrada;
    private Vector3 posicionAbierta;
    private bool estaAbierta = false;
    private bool enMovimiento = false;
    private float tiempoEspera = 0f;

    void Start()
    {
        // Guardar la posición inicial (cerrada)
        posicionCerrada = puertaCentral.transform.localPosition;
        // Calcular la posición abierta
        posicionAbierta = posicionCerrada + (Vector3.up * distanciaApertura);
    }

    void Update()
    {
        // Detectar jugador cerca
        bool jugadorCerca = Physics.CheckSphere(transform.position, radioDeteccion, capaJugador);

        if (jugadorCerca && !estaAbierta && !enMovimiento)
        {
            StartCoroutine(AbrirPuerta());
        }
        else if (!jugadorCerca && estaAbierta && !enMovimiento)
        {
            tiempoEspera += Time.deltaTime;
            if (tiempoEspera >= tiempoEsperaAutoCierre)
            {
                StartCoroutine(CerrarPuerta());
                tiempoEspera = 0f;
            }
        }
    }

    System.Collections.IEnumerator AbrirPuerta()
    {
        enMovimiento = true;
        float tiempoPasado = 0f;
        Vector3 posicionInicial = puertaCentral.transform.localPosition;

        while (tiempoPasado < 1f)
        {
            tiempoPasado += Time.deltaTime * velocidadMovimiento;
            puertaCentral.transform.localPosition = Vector3.Lerp(posicionInicial, posicionAbierta, tiempoPasado);
            yield return null;
        }

        estaAbierta = true;
        enMovimiento = false;
    }

    System.Collections.IEnumerator CerrarPuerta()
    {
        enMovimiento = true;
        float tiempoPasado = 0f;
        Vector3 posicionInicial = puertaCentral.transform.localPosition;

        while (tiempoPasado < 1f)
        {
            tiempoPasado += Time.deltaTime * velocidadMovimiento;
            puertaCentral.transform.localPosition = Vector3.Lerp(posicionInicial, posicionCerrada, tiempoPasado);
            yield return null;
        }

        estaAbierta = false;
        enMovimiento = false;
    }

    // Método para visualizar el radio de detección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }

    // Métodos públicos para control manual
    public void AbrirPuertaManualmente()
    {
        if (!estaAbierta && !enMovimiento)
        {
            StartCoroutine(AbrirPuerta());
        }
    }

    public void CerrarPuertaManualmente()
    {
        if (estaAbierta && !enMovimiento)
        {
            StartCoroutine(CerrarPuerta());
        }
    }
}