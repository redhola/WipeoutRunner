using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float delay = 2f; // Bu süreyi efektin süresine göre ayarlayın.

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
