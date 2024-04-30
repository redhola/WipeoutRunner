using UnityEngine;

public class MutliplierSpawner : MonoBehaviour
{
    public GameObject followerPrefab; // Assign this in the inspector
    public int spawnMultiplier; 
    public GameObject spawnera;
    public GameObject spawnerb;
    public float spacing = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger area
        if (other.CompareTag("Player"))
        {
            // Retrieve the total number of followers from the GameManager
            int totalFollowers = GameManager.Instance.followerCount;
            // Calculate the spawn count based on the total followers and multiplier from the inspector
            int spawnCount = totalFollowers * (spawnMultiplier - 1);
            
            // Spawn the followers
            SpawnFollowers(spawnCount); // Use the new function to spawn followers in a grid

            // Optionally deactivate the spawner objects
            spawnera.SetActive(false);
            spawnerb.SetActive(false);
        }
    }

    void SpawnFollowers(int spawnCount) // Function updated to handle grid spawning
    {
        int rowSize = Mathf.CeilToInt(Mathf.Sqrt(spawnCount)); // Determine the number of rows for the grid

        for (int i = 0; i < spawnCount; i++) 
        {
            int row = i / rowSize;
            int col = i % rowSize;

            // Arrange followers in a grid formation with a set distance between each
            Vector3 spawnPos = transform.position + (transform.right * spacing * col * 2) - (transform.forward * spacing * row * 2);
            Quaternion spawnRot = Quaternion.LookRotation(Vector3.forward); //Faces down the world z-axis
            Instantiate(followerPrefab, spawnPos, spawnRot);
            GameManager.Instance.GainFollower(1);
        }
    }

}
