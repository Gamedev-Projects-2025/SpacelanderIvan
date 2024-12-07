using UnityEngine;

public class DynamicGravity : MonoBehaviour
{
    [Header("Gravity Settings")]
    public Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D
    public GameObject ground;           // Reference to the ground GameObject
    public float maxHeight = 10f;       // The height at which gravity is zero

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
        float groundY = ground.transform.position.y;
        float playerY = transform.position.y;
        float distanceFromGround = Mathf.Clamp(playerY - groundY, 0, maxHeight);

        // Calculate the gravity scale based on the height
        float gravityScale = Mathf.Lerp(1f, 0f, distanceFromGround / maxHeight);

        // Apply the gravity scale to the Rigidbody2D
        playerRigidbody.gravityScale = gravityScale;
    }
}
