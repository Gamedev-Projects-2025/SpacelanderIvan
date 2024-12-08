using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float thrustPower = 10f; // Forward/backward thrust
    [SerializeField] private float torquePower = 5f; // Rotational torque

    [Header("Landing Settings")]
    [SerializeField] private float maxLandingSpeed = 5f; // Max speed to land safely

    [SerializeField] private NumberField speedCounter;
    [SerializeField] private NumberField maxSpeedCounter;
    [SerializeField] private NumberField timer;

    [SerializeField] private float rotationThreshold = 90f;

    [SerializeField] private float completionTime = 3f;

    [SerializeField] private string sceneName, gameOver;

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
            loadGameOver();
        }
    }

    void CheckLanding(float speed)
    {
        // Check if the spaceship is upright (orientation close to zero rotation)
         // Allowed rotation difference in degrees
        float currentRotation = Mathf.Abs(transform.eulerAngles.z % 360); // Z-axis rotation

        // Normalize rotation to be between -180 and 180
        if (currentRotation > 180)
            currentRotation -= 360;

        if (speed <= maxLandingSpeed &&
            Mathf.Abs(currentRotation) <= rotationThreshold &&
            gameObject.GetComponent<disappear>().isTargetDeployed())
        {
            // Successfully landed
            Debug.Log("Landed safely!");

            // Stop the spaceship's motion
            thrustPower = 0f;
            torquePower = 0f;

            // Start a timer to ensure the player stays landed
            StartCoroutine(LevelCompletionTimer());
        }
        else
        {
            // Crash landing
            Debug.Log("Crash landing! Speed, rotation, or target deployment incorrect.");
            loadGameOver();
        }
    }

    // Coroutine to ensure the player stays landed for a duration to finish the level
    private IEnumerator LevelCompletionTimer()
    {

        float elapsedTime = 0f;

        while (elapsedTime < completionTime)
        {
            elapsedTime += Time.deltaTime;
            timer.SetNumber((int)(completionTime - elapsedTime));
            yield return null;
        }

        Debug.Log("Level completed!");
        loadNextLevel();
        // Add your level completion logic here (e.g., load the next level)
    }

    private void loadNextLevel()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void loadGameOver()
    {
        if (!string.IsNullOrEmpty(gameOver))
        {
            SceneManager.LoadScene(gameOver);
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