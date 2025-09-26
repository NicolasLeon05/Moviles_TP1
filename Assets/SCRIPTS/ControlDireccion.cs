using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CarController))]
[RequireComponent(typeof(PlayerInput))]
public class ControlDireccion : MonoBehaviour
{
    private CarController carController;
    private PlayerInput playerInput;
    private InputAction steerAction;

    private float giro;
    public bool habilitado = true;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        playerInput = GetComponent<PlayerInput>();

        // Busca la acción "Steer" en el mapa "Driving"
        steerAction = playerInput.actions["Steer"];
    }

    private void OnEnable()
    {
        steerAction.Enable();
    }

    private void OnDisable()
    {
        steerAction.Disable();
    }

    private void Update()
    {
        if (!habilitado)
            return;

        giro = steerAction.ReadValue<float>();

        carController.SetGiro(giro);
    }

    public float GetGiro()
    {
        return giro;
    }
}
