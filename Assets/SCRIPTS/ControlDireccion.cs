using UnityEngine;

[RequireComponent(typeof(CarController))]
public class ControlDireccion : MonoBehaviour
{
    private CarController carController;
    private InputSystem_Actions actions;

    private float giro;
    public bool habilitado = true;
    public int playerId = 0; // 0 = P1, 1 = P2

    private void Start()
    {
        carController = GetComponent<CarController>();
        TryInit();
    }

    private void TryInit()
    {
        if (actions == null && GameManager.Instancia != null)
            actions = GameManager.Instancia.actions;
    }

    private void Update()
    {
        TryInit();

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
