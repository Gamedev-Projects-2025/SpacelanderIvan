using UnityEngine;
using UnityEngine.InputSystem;

public class disappear : MonoBehaviour
{
    [SerializeField]
    private InputAction destroyer = new InputAction(type: InputActionType.Button);

    [SerializeField]
    private GameObject target; //gameobject we want to disappear

    private void OnEnable()
    {
        destroyer.Enable();
    }

    private void OnDisable()
    {
        destroyer.Disable();
    }

    private void Start()
    {
        
    }

    void Update()
    {
        //to prevent the object from appearing and disappearing rapidly, we use WasPerformedThisFrame instead of isPressed
        if (destroyer.WasPerformedThisFrame())
        {
            target.SetActive(!target.activeSelf);
        }
    }

    public bool isTargetDeployed()
    {
        return target.activeSelf;
    }
}
