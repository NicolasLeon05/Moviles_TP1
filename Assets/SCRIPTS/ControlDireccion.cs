using UnityEngine;

[RequireComponent(typeof(CarController))]
public class ControlDireccion : MonoBehaviour
{
    private CarController carController;
    private InputSystem_Actions actions;

    private float giro;
    public bool habilitado = true;
    public int playerId = 0; 

    private void Awake()
    {
        carController = GetComponent<CarController>();
        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        actions.Driving.Enable();
    }

    private void OnDisable()
    {
        actions.Driving.Disable();
    }

    private void OnDestroy()
    {
        actions.Dispose();
    }

    private void Update()
    {
        if (!habilitado)
            return;

        if (playerId == 0)
            giro = actions.Driving.SteerP1.ReadValue<float>();
        else
            giro = actions.Driving.SteerP2.ReadValue<float>();

        carController.SetGiro(giro);
    }

    public float GetGiro()
    {
        return giro;
    }
}
