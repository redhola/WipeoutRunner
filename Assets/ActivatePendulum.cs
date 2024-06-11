using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePendulum : MonoBehaviour
{
    public GameObject pendulum; // Reference to the pendulum object
    public float speed = 1.5f;
    public float limit = 75f; // Limit in degrees of the movement

    private bool isActivated = false; // Pendulum activation status

    // Start is called before the first frame update
    void Start()
    {
        // Start with a fixed angle
        if (pendulum != null)
        {
            pendulum.transform.localRotation = Quaternion.Euler(0, 0, limit * Mathf.Sin(0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && pendulum != null)
        {
            float angle = limit * Mathf.Sin(Time.time * speed);
            pendulum.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assuming player is the tag that triggers the pendulum
        {
            isActivated = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActivated = false;
        }
    }
}
