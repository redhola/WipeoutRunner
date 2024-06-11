using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulumm : MonoBehaviour
{
    public float speed = 1.5f;
    public float limit = 75f; // Limit in degrees of the movement
    public string activationTag = "ActivatePendulum"; // Tag of the collider that activates the pendulum
    private bool isActivated = false; // Pendulum activation status

    // Start is called before the first frame update
    void Start()
    {
        // Start with a fixed angle
        transform.localRotation = Quaternion.Euler(0, 0, limit * Mathf.Sin(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            float angle = limit * Mathf.Sin(Time.time * speed);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(activationTag))
        {
            isActivated = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(activationTag))
        {
            isActivated = false;
        }
    }
}
