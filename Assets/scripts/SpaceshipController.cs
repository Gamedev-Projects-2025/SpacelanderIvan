using TMPro;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float thrustPower = 10f; // Forward/backward thrust
    [SerializeField] private float torquePower = 5f; // Rotational torque

    [Header("Landing Settings")]
    [SerializeField] private float maxLandingSpeed = 5f; // Max speed to land safely

    [SerializeField] private NumberField speedCounter;
    [SerializeField] private NumberField maxSpeedCounter;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        speedCounter.SetNumber((int)CurrentSpeed);
        maxSpeedCounter.SetNumber((int)MaxLandingSpeed);
        HandleInput();
    }

    void HandleInput()
    {
        // Apply thrust
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * thrustPower);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-transform.up * thrustPower);
        }

        // Apply torque
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(torquePower);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-torquePower);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CheckLanding(collision.relativeVelocity.magnitude);
        }
        else
        {
            Debug.Log("Crashed into an obstacle!");
        }
    }

    void CheckLanding(float speed)
    {
        if (speed <= maxLandingSpeed && gameObject.GetComponent<disappear>().isTargetDeployed())
        {
            Debug.Log("Landed safely!");
        }
        else
        {
            Debug.Log("Crash landing! Speed or rotation too high.");
        }
    }

    public float CurrentSpeed
    {
        get
        {
            return rb.linearVelocity.magnitude;
        }
    }

    public float MaxLandingSpeed
    {
        get
        {
            return maxLandingSpeed;
        }
    }
}