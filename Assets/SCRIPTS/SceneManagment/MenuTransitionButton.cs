using UnityEngine;

public class MenuTransitionButton : MonoBehaviour
{
    [SerializeField] private Menu targetMenu;
    [SerializeField] private NavigationController navigationController;


    public void ActivateBaseMenu()
    {
        GameEvents.TriggerActivateBaseMenu();
    }

    /// <summary>
    /// Triggers an event that sets the targetMenu as active
    /// and the current state as the stateToTransition
    /// </summary>
    public void ActivateMenu()
    {
        Debug.Log("Botón Return presionado, target: " + targetMenu?.name);
        if (targetMenu != null)
            GameEvents.TriggerActivateMenu(targetMenu);
    }

    /// <summary>
    /// Calls the SetAllInactive() function from NavigationController.
    /// Sets the GameManager state to the assigned target state
    /// </summary>
    public void SetAllInactive()
    {
        GameEvents.TriggerSetAllMenusInactive();
    }
}
