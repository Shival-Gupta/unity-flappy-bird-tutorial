using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject[] obstaclePrefabs;  // Array of obstacle prefabs (Asteroids, Balloons, etc.)
    public float spawnRate = 1f;          // General spawn rate for all obstacles
    public float minHeight = -1f;         // Min height for obstacle spawn
    public float maxHeight = 2f;          // Max height for obstacle spawn

    private void OnEnable()
    {
        if (obstaclePrefabs.Length == 0)
        {
            Debug.LogWarning("No obstacle prefabs assigned to the spawner!");
            return;
        }

        // Start spawning obstacles at the general spawn rate
        StartCoroutine(SpawnObstacles());
    }

    private void OnDisable()
    {
        // Stop the spawning when the spawner is disabled
        StopAllCoroutines();
    }

    private IEnumerator SpawnObstacles()
    {
        // Spawn obstacles at the general spawn rate
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnRate);  // Wait for the general spawn rate
        }
    }

    private void SpawnObstacle()
    {
        // Select a random obstacle prefab from the array
        GameObject selectedObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Instantiate the selected prefab at the spawner's position
        GameObject obstacle = Instantiate(selectedObstacle, transform.position, Quaternion.identity);

        // Randomize the vertical position of the obstacle
        float randomHeight = Random.Range(minHeight, maxHeight);
        obstacle.transform.position += Vector3.up * randomHeight;
    }
}
