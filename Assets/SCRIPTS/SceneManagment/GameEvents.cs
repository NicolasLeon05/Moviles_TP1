using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnReturnToMainMenu;
    public static event Action OnActivateBaseMenu;
    public static event Action<Menu> OnActivateMenu;
    public static event Action OnSetAllMenusInactive;
    public static event Action OnShowCredits;

    public static void TriggerReturnToMainMenu()
    {
        OnReturnToMainMenu?.Invoke();
    }

    public static void TriggerActivateBaseMenu()
    {
        OnActivateBaseMenu?.Invoke();
    }

    public static void TriggerActivateMenu(Menu menu)
    {
        OnActivateMenu?.Invoke(menu);
    }

    public static void TriggerSetAllMenusInactive()
    {
        OnSetAllMenusInactive?.Invoke();
    }

    public static void TriggerShowCredits()
    {
        OnShowCredits?.Invoke();
    }
}
