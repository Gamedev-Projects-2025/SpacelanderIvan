using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField]
    private float amplitude = 1f;

    [SerializeField] 
    private float frequency = 1f;

    [SerializeField] 
    private Vector3 oscillationAxis = Vector3.right;

    [SerializeField] 
    private float phaseOffset = 0f;

    private Vector3 startPosition;

    void Start()
    {
        //getting the initial position
        startPosition = transform.position;
    }

    void Update()
    {
        //calculating the offset based on a sin function
        float displacement = Mathf.Sin((Time.time + phaseOffset) * frequency * 2 * Mathf.PI) * amplitude;

        //applying offset
        transform.position = startPosition + oscillationAxis.normalized * displacement;
    }
}