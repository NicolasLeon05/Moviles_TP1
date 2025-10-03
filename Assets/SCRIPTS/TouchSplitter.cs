using UnityEngine;
using UnityEngine.InputSystem;

public class TouchSplitter : MonoBehaviour
{
    public static float GetPlayerInput(int playerId)
    {
        if (Touchscreen.current == null) 
            return 0f;

        foreach (var touch in Touchscreen.current.touches)
        {
            if (!touch.press.isPressed) 
                continue;

            Vector2 pos = touch.position.ReadValue();

            if (playerId == 0 && pos.x < Screen.width / 2)
            {
                return (pos.x / (Screen.width / 2)) * 2f - 1f;
            }
            else if (playerId == 1 && pos.x >= Screen.width / 2)
            {
                return ((pos.x - Screen.width / 2) / (Screen.width / 2)) * 2f - 1f;
            }
        }

        return 0f;
    }
}
