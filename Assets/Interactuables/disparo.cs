using UnityEngine;

public class disparo : MonoBehaviour
{
    [Header("Configuración Básica")]
    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;

    [Header("Sistema de Munición")]
    public int tamañoCargador = 30;
    public int municionTotal = 120;
    public int municionEnCargador;
    public float tiempoRecarga = 2f;

    private float shotRateTime = 0;
    private bool estaRecargando = false;

    // Referencias de UI (opcional)
    [Header("UI Referencias")]
    public TMPro.TextMeshProUGUI textoMunicion; // Asegúrate de importar TMPro si usas esto

    void Start()
    {
        municionEnCargador = tamañoCargador;
        ActualizarUIAmmo();
    }

    void Update()
    {
        // Si está recargando, no permite disparar
        if (estaRecargando)
            return;

        // Sistema de recarga
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Recargar());
            return;
        }

        // Sistema de disparo
        if (Input.GetButtonDown("Fire1") && Time.time > shotRateTime && municionEnCargador > 0)
        {
            Disparar();
        }
    }

    void Disparar()
    {
        GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
        shotRateTime = Time.time + shotRate;
        Destroy(newBullet, 5);

        municionEnCargador--;
        ActualizarUIAmmo();

        // Recarga automática cuando el cargador está vacío
        if (municionEnCargador <= 0 && municionTotal > 0)
        {
            StartCoroutine(Recargar());
        }
    }

    System.Collections.IEnumerator Recargar()
    {
        if (municionTotal <= 0 || municionEnCargador >= tamañoCargador)
            yield break;

        estaRecargando = true;

        Debug.Log("Recargando...");

        // Esperar el tiempo de recarga
        yield return new WaitForSeconds(tiempoRecarga);

        // Calcular cuánta munición se necesita para llenar el cargador
        int municionNecesaria = tamañoCargador - municionEnCargador;
        int municionARecargar = Mathf.Min(municionNecesaria, municionTotal);

        municionTotal -= municionARecargar;
        municionEnCargador += municionARecargar;

        estaRecargando = false;
        ActualizarUIAmmo();

        Debug.Log("Recarga completada");
    }

    void ActualizarUIAmmo()
    {
        if (textoMunicion != null)
        {
            textoMunicion.text = $"{municionEnCargador}/{municionTotal}";
        }
    }

    // Método público para añadir munición (útil para recoger munición)
    public void AñadirMunicion(int cantidad)
    {
        municionTotal += cantidad;
        ActualizarUIAmmo();
    }
}