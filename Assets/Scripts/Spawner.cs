using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject followerPrefab;
    public Transform player; 
    public int spawnCount = 5; // Number of followers to spawn
    public GameObject spawnera;
    public GameObject spawnerb;
    public float spacing = 1.0f;

    private void Start() 
    {
        // Find the player by tag, only if not assigned
        if (player == null) 
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player not found! Make sure your player GameObject has the 'Player' tag.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger area
        if (other.CompareTag("Player"))
        {
            SpawnFollower();
            spawnera.SetActive(false);
            spawnerb.SetActive(false);
        }
    }

    void SpawnFollower()
    {
        int rowSize = Mathf.CeilToInt(Mathf.Sqrt(spawnCount)); 

        for (int i = 0; i < spawnCount; i++)
        {
            int row = i / rowSize; 
            int col = i % rowSize; 

            // To spawn behind, we subtract the forward direction vector times the row index and spacing
            Vector3 spawnPos = transform.position + (transform.right * spacing * col*3) - (transform.forward * spacing * row);
            Quaternion spawnRot = Quaternion.LookRotation(Vector3.forward); 
            Instantiate(followerPrefab, spawnPos, spawnRot);
            GameManager.Instance.GainFollower(1);
        }
    }
}
