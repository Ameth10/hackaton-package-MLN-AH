using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MLNFeedbacks.Demo
{
    /// <summary>
    /// Lecture du clic et de la touche F, qui marche avec le nouvel Input System
    /// comme avec l'ancien (selon les réglages du projet).
    /// </summary>
    static class DemoInput
    {
        public static bool ClickedThisFrame(out Vector2 position)
        {
#if ENABLE_INPUT_SYSTEM
            Mouse mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.wasPressedThisFrame)
            {
                position = mouse.position.ReadValue();
                return true;
            }
            position = default;
            return false;
#else
            position = Input.mousePosition;
            return Input.GetMouseButtonDown(0);
#endif
        }

        public static bool TogglePressedThisFrame()
        {
#if ENABLE_INPUT_SYSTEM
            return Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame;
#else
            return Input.GetKeyDown(KeyCode.F);
#endif
        }
    }
}
