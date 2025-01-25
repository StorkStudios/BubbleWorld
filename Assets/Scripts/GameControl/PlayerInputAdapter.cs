using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAdapter : MonoBehaviour
{
    [SerializeField]
    private DialogueBox dialogueBox;

    public void Skip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dialogueBox.Skip();
        }
    }
}
