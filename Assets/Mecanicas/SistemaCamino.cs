using UnityEngine;
using System.Collections.Generic;

public class SistemaCamino : MonoBehaviour
{
    [Header("Configuración del Sistema")]
    [SerializeField] private Transform[] puntosCamino;
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float distanciaMinima = 0.1f;
    
    private int puntoActual = 0;
    private bool moviendose = true;

    void Start()
    {
        if (puntosCamino.Length == 0)
        {
            Debug.LogWarning("No hay puntos de camino asignados.");
            moviendose = false;
        }
    }

    void Update()
    {
        if (!moviendose || puntosCamino.Length == 0) return;

        // Obtener la posición objetivo actual
        Vector3 posicionObjetivo = puntosCamino[puntoActual].position;
        
        // Mover hacia el punto objetivo
        transform.position = Vector3.MoveTowards(
            transform.position, 
            posicionObjetivo, 
            velocidadMovimiento * Time.deltaTime
        );

        // Verificar si llegamos al punto actual
        if (Vector3.Distance(transform.position, posicionObjetivo) < distanciaMinima)
        {
            // Pasar al siguiente punto
            puntoActual = (puntoActual + 1) % puntosCamino.Length;
        }
    }

    // Método para visualizar el camino en el editor
    private void OnDrawGizmos()
    {
        if (puntosCamino == null || puntosCamino.Length == 0) return;

        // Dibujar líneas entre los puntos
        for (int i = 0; i < puntosCamino.Length - 1; i++)
        {
            if (puntosCamino[i] != null && puntosCamino[i + 1] != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(puntosCamino[i].position, puntosCamino[i + 1].position);
            }
        }

        // Conectar el último punto con el primero si hay más de un punto
        if (puntosCamino.Length > 1 && puntosCamino[0] != null && 
            puntosCamino[puntosCamino.Length - 1] != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(
                puntosCamino[puntosCamino.Length - 1].position, 
                puntosCamino[0].position
            );
        }
    }

    // Métodos de control
    public void PausarMovimiento()
    {
        moviendose = false;
    }

    public void ReanudarMovimiento()
    {
        moviendose = true;
    }

    public void EstablecerVelocidad(float nuevaVelocidad)
    {
        velocidadMovimiento = nuevaVelocidad;
    }
}