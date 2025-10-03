using UnityEngine;
using UnityEngine.InputSystem;

public static class TouchSplitterUnloading
{
    private static Vector2[] startPos = new Vector2[2];
    private static bool[] isSwiping = new bool[2];

    public static string GetPlayerAction(int playerId)
    {
        if (Touchscreen.current == null) return null;

        foreach (var touch in Touchscreen.current.touches)
        {
            if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                Vector2 pos = touch.position.ReadValue();

                if ((playerId == 0 && pos.x < Screen.width / 2) ||
                    (playerId == 1 && pos.x >= Screen.width / 2))
                {
                    startPos[playerId] = pos;
                    isSwiping[playerId] = true;
                }
            }

            if (isSwiping[playerId] && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                Vector2 endPos = touch.position.ReadValue();
                Vector2 delta = endPos - startPos[playerId];

                if (delta.magnitude < 50f) 
                    return null;

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (delta.x > 0) 
                        return "Third";   // derecha
                    else 
                        return "First";               // izquierda
                }
                else
                {
                    if (delta.y < 0) 
                        return "Second";  // abajo
                }

                isSwiping[playerId] = false;
            }
        }

        return null;
    }
}
