using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("¡Colisión detectada (trigger)!");
        // Puedes realizar acciones adicionales aquí
    }
}
