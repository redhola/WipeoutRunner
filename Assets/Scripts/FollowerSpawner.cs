using UnityEngine;

public class FollowerSpawner : MonoBehaviour
{
    public GameObject followerPrefab; // Assign this in the inspector
    public float spawnDelay = 2f; // Time between spawns
    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnFollower();
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    void SpawnFollower()
    {
        // Calculate the spawn position behind the player
        Vector3 spawnPos = transform.position - transform.forward * 2; // Adjust the multiplier as needed
        Quaternion spawnRot = Quaternion.identity; // Followers will face the original rotation
        Instantiate(followerPrefab, spawnPos, spawnRot);
    }
}
