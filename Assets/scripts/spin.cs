using UnityEngine;

public class spin : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1f;

    [SerializeField]
    private Vector3 rotationAxis = Vector3.forward;

    void Update()
    {
        //Rorating the object around its origin
        transform.Rotate(rotationAxis.normalized, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
