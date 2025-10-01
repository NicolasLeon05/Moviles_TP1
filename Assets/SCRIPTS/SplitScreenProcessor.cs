using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Processors;

[InputControlLayout(displayName = "SplitScreen Touch")]
public class SplitScreenProcessor : InputProcessor<Vector2>
{
    public override Vector2 Process(Vector2 value, InputControl control)
    {
        if (value == Vector2.zero)
            return Vector2.zero;

        var screenWidth = Screen.width;
        bool isLeft = value.x < screenWidth / 2f;

        return isLeft ? new Vector2(-1, 0) : new Vector2(1, 0);
    }
}
