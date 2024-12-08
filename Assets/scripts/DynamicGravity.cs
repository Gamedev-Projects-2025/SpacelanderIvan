using UnityEngine;

public class DynamicGravity : MonoBehaviour
{
    [Header("Gravity Settings")]

    [SerializeField]  private Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D
    [SerializeField]  private GameObject ground, gravityBoundry;           // Reference to the ground GameObject
    private float maxHeight;       // The height at which gravity is zero

    private void Start()
    {
        maxHeight = gravityBoundry.transform.position.y - ground.transform.position.y; 
    }

    void FixedUpdate()
    {
        UpdateGravityScale();
    }

    void UpdateGravityScale()
    {
        if (!playerRigidbody || !ground)
        {
            Debug.LogWarning("Player Rigidbody or Ground reference is missing!");
            return;
        }

        // Get the current height of the player relative to the ground
        float distanceFromGround = Mathf.Clamp(transform.position.y - ground.transform.position.y, 0, maxHeight);

        // Calculate the gravity scale based on the height
        float gravityScale = Mathf.Lerp(1f, 0f, distanceFromGround / maxHeight);

        // Apply the gravity scale to the Rigidbody2D
        playerRigidbody.gravityScale = gravityScale;
    }
}
