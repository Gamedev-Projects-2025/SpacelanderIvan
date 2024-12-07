using UnityEngine;

public class heartbeat : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 1f;

    [SerializeField]
    private float frequency = 1f;

    [SerializeField]
    private Vector3 oscillationAxis = new Vector3(1, 1, 1);

    [SerializeField]
    private float phaseOffset = 0f;

    private Vector3 startScale;

    void Start()
    {
        //getting the initial scale
        startScale = transform.localScale;
    }

    void Update()
    {
        //reusing the oscillator code on the scale instead of the coordinates
        float scale = Mathf.Sin((Time.time + phaseOffset) * frequency * 2 * Mathf.PI) * amplitude;

        //applying the new scale
        transform.localScale = startScale + oscillationAxis.normalized * scale;
    }
}
