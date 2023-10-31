using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a instanciar
    public float spawnInterval = 1f; // Intervalo entre instancias

    private float spawnXMin = -8f; // Límite izquierdo de la pantalla
    private float spawnXMax = 8f; // Límite derecho de la pantalla
    private void Start()
    {
        StartCoroutine(SpawnEnemyRepeatedly());
    }

    private IEnumerator SpawnEnemyRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector3 spawnPosition = new Vector3(Random.Range(spawnXMin, spawnXMax), 8f, 0f);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
