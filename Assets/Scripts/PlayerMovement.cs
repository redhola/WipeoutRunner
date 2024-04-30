using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float lateralSpeed = 5f;
    private Rigidbody rb;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isTouching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for screen touches if it is a mobile platform or the Unity remote is being used.
#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isTouching = true;
                    break;
                case TouchPhase.Moved:
                    currentTouchPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    isTouching = false;
                    startTouchPosition = currentTouchPosition = Vector2.zero; // Reset the positions
                    break;
            }
        }
#endif
    }

    void FixedUpdate()
    {
        // Forward movement remains constant
        rb.MovePosition(rb.position + Vector3.forward * forwardSpeed * Time.fixedDeltaTime);

        // Determine lateral movement based on platform
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        // Keyboard input
        float keyboardLateralInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        Vector3 keyboardLateralMovement = Vector3.right * keyboardLateralInput * lateralSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + keyboardLateralMovement);
#elif UNITY_IOS || UNITY_ANDROID
        // Touch input
        if (isTouching)
        {
            // Calculate lateral movement based on touch
            float lateralInput = (currentTouchPosition - startTouchPosition).x;

            // Normalize lateralInput to be no greater than 1 or less than -1
            lateralInput = Mathf.Clamp(lateralInput / Screen.width, -1f, 1f);

            Vector3 lateralMovement = Vector3.right * lateralInput * lateralSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + lateralMovement);
        }
#endif
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.LoseFollower(5);
        }
    }
}
