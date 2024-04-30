using UnityEngine;
using UnityEngine.AI; // Required for manipulating NavMeshAgent



public class PlayerTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoadFollower"))
        {
            // Mevcut mantığı koruyoruz
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            if (agent != null && !agent.enabled)
            {
                agent.enabled = true; 
                agent.SetDestination(transform.position);
                GameManager.Instance.GainFollower(1);
            }
        }
        if (other.CompareTag("Obstacle"))
        {
            // Sahnedeki tüm follower'ları bul
            GameObject[] allFollowers = GameObject.FindGameObjectsWithTag("SpawnFollower");
            if (allFollowers.Length > 0)
            {
                int randomIndex = Random.Range(0, allFollowers.Length);
                Destroy(allFollowers[randomIndex]);              
            }
            GameManager.Instance.LoseFollower(1);
        }
    }
}
