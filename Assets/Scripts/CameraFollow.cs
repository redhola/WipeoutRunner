using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset; 
    void LateUpdate()
    {
        transform.position = new Vector3(offset.x, offset.y, player.position.z + offset.z);
    }
}
