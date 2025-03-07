using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAdapter : Singleton<PlayerInputAdapter>
{
    [SerializeField]
    private DialogueBox dialogueBox;

    private bool skipHeld = false;

    public bool SkipHeld => skipHeld;

    public void Skip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dialogueBox.Skip();
        }
    }

    public void SkipHold(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            skipHeld = true;
        }
        else if (context.canceled)
        {
            skipHeld = false;
        }
    }
}
