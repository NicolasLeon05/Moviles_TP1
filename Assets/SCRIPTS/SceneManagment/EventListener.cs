using UnityEngine;

public class EventListener : MonoBehaviour
{
    [SerializeField] private NavigationController navigationController;
    [SerializeField] private Menu creditsMenu;

    private Menu baseMenu;

    private void Start()
    {
        baseMenu = navigationController.baseMenu;
    }

    private void OnEnable()
    {
        GameEvents.OnReturnToMainMenu += HandleReturnToMainMenu;
        GameEvents.OnActivateBaseMenu += HandleActivateBaseMenu;
        GameEvents.OnActivateMenu += HandleActivateMenu;
        GameEvents.OnSetAllMenusInactive += HandleSetAllMenusInactive;
        GameEvents.OnShowCredits += HandleShowCredits;
    }


    private void OnDisable()
    {
        GameEvents.OnReturnToMainMenu -= HandleReturnToMainMenu;
        GameEvents.OnActivateBaseMenu -= HandleActivateBaseMenu;
        GameEvents.OnActivateMenu -= HandleActivateMenu;
        GameEvents.OnSetAllMenusInactive -= HandleSetAllMenusInactive;
        GameEvents.OnShowCredits -= HandleShowCredits;
    }

    private void HandleReturnToMainMenu()
    {
        SceneController.Instance.UnloadNonPersistentScenes();
    }

    private void HandleActivateBaseMenu()
    {
        navigationController.SetMenuActive(baseMenu);
    }

    private void HandleActivateMenu(Menu menu)
    {
        Debug.Log("HandleActivateMenu recibido para: " + menu?.name);
        navigationController.SetMenuActive(menu);
    }

    private void HandleSetAllMenusInactive()
    {
        navigationController.SetAllInactive();
    }

    private void HandleShowCredits()
    {
        SceneController.Instance.UnloadNonPersistentScenes();
        navigationController.SetMenuActive(creditsMenu);
    }
}
