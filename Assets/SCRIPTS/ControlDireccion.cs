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
        if (actions == null && GameManager.Instance != null)
            actions = GameManager.Instance.actions;
    }

    private void Update()
    {
        TryInit();

        if (!habilitado)
            return;

#if UNITY_STANDALONE || UNITY_EDITOR
        if (playerId == 0)
            giro = actions.Driving.SteerP1.ReadValue<float>();
        else
            giro = actions.Driving.SteerP2.ReadValue<float>();
#elif UNITY_ANDROID || UNITY_IOS
        giro = TouchSplitter.GetPlayerInput(playerId);
#endif
        carController.SetGiro(giro);
    }

    public float GetGiro()
    {
        return giro;
    }
}
