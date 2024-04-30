using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObs : MonoBehaviour
{
    public float distance = 15f; //Distance that moves the object
    public bool horizontal = true; //If the movement is horizontal or vertical
    public float speed = 3f;
    public float offset = 0f; //If you want to modify the position at the start 

    private bool isForward = true; //If the movement is out
    private Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
        if (horizontal)
        {
            transform.position += Vector3.left * offset; // Hareketi sola ayarla
        }
        else
        {
            transform.position += Vector3.back * offset; // Değişiklik yok, ancak forward yerine back kullanılabilir.
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal)
        {
            if (isForward)
            {
                // Sol taraf için pozisyonu güncelle
                if (transform.position.x > startPos.x - distance) 
                {
                    transform.position += Vector3.left * Time.deltaTime * speed;
                }
                else
                    isForward = false;
            }
            else
            {
                // Sağa geri dönüş için pozisyonu güncelle
                if (transform.position.x < startPos.x)
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                }
                else
                    isForward = true;
            }
        }
        else
        {
            // Dikey hareket için kod değişmedi.
            if (isForward)
            {
                if (transform.position.z < startPos.z + distance)
                {
                    transform.position += Vector3.forward * Time.deltaTime * speed;
                }
                else
                    isForward = false;
            }
            else
            {
                if (transform.position.z > startPos.z)
                {
                    transform.position -= Vector3.forward * Time.deltaTime * speed;
                }
                else
                    isForward = true;
            }
        }
    }
}
