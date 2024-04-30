using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class FollowerBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public ParticleSystem deathEffect; // Follower yok olduğunda tetiklenecek efekt.
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player == null)
            {
                Debug.LogError("Player transform is not found! Make sure your player has the 'Player' tag.");
            }
        }
        StartCoroutine(SetInitialDestination());
    }

    public IEnumerator SetInitialDestination()
    {
        // Wait until the end of the frame
        yield return new WaitForEndOfFrame();

        // Ensure the agent is on the NavMesh before setting the destination
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            Debug.LogError(gameObject.name + " is not on the NavMesh.");
        }
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is lost during gameplay on " + gameObject.name);
            return;
        }
        agent.SetDestination(player.transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            InstantiateDeathEffect();
            GameManager.Instance.LoseFollower(1);
            Destroy(gameObject);
        }
    }

    private void InstantiateDeathEffect()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
