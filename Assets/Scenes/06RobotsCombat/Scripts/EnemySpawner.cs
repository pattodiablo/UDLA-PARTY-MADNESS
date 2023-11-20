using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f; // Intervalo de tiempo entre apariciones de enemigos
    public float enemySpeed = 2f; // Velocidad de desplazamiento de los enemigos
    public float screenTopOffset = 1f; // Offset desde la parte superior de la pantalla para que los enemigos aparezcan

    public Camera mainCamera;
    void Start()
    {
        // Invocar repetidamente la función SpawnEnemy con el intervalo especificado
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Calcular la posición de aparición en la parte superior de la pantalla
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), mainCamera.transform.position.x + screenTopOffset, 10f);

        // Instanciar el enemigo en la posición de aparición
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Obtener el componente Rigidbody del enemigo y aplicarle una velocidad hacia abajo
        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
        if (enemyRigidbody != null)
        {
            enemyRigidbody.velocity = Vector3.down * enemySpeed;
        }
    }
}
