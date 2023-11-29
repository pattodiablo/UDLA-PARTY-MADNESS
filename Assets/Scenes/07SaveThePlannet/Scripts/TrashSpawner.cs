using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trashObjectPrefab;  // Arrastra tu prefab "TrashObject" a este campo en el Inspector.
    public float spawnInterval = 2f;      // Intervalo de tiempo entre apariciones en segundos.
    public int maxTrashObjects = 20;      // Número máximo de TrashObjects permitidos en pantalla.
    public float verticalSpawnOffset = 1f; // Variación vertical desde el centro de la pantalla.

    private float nextSpawnTime;
    private List<GameObject> trashObjects = new List<GameObject>();

    void Update()
    {
        // Verifica si es el momento de generar un nuevo TrashObject
        if (Time.time >= nextSpawnTime && trashObjects.Count < maxTrashObjects)
        {
            SpawnTrashObject();
            // Establece el próximo tiempo de aparición
            nextSpawnTime = Time.time + spawnInterval;
        }

        // Elimina los objetos más antiguos si se supera el límite
        if (trashObjects.Count > maxTrashObjects)
        {
            DestroyOldestTrashObject();
        }
    }

    void SpawnTrashObject()
    {
        // Calcula la posición de aparición en el centro de la pantalla con variación vertical
        float screenHeight = Camera.main.orthographicSize * 2;
        float spawnY = Random.Range(-verticalSpawnOffset, verticalSpawnOffset);

        // Calcula la posición de aparición en el lado izquierdo de la pantalla
        float screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        Vector3 spawnPosition = new Vector3(transform.position.x - screenWidth / 2, spawnY, transform.position.z);

        // Instancia el TrashObject en la posición calculada
        GameObject newTrashObject = Instantiate(trashObjectPrefab, spawnPosition, Quaternion.identity);

        // Agrega el objeto a la lista
        trashObjects.Add(newTrashObject);

        // Opcionalmente, puedes establecer propiedades o comportamientos adicionales para el objeto generado aquí
    }

    void DestroyOldestTrashObject()
    {
        // Destruye el objeto más antiguo en la lista
        GameObject oldestObject = trashObjects[0];
        trashObjects.RemoveAt(0);
        Destroy(oldestObject);
    }
}
    