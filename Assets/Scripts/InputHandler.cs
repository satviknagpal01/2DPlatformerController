using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerController player;
    CharacterInputs _controller;

    private void OnEnable()
    {
        if (_controller == null)
        {
            _controller = new();
            _controller.Character.Walk.performed += i => player.InputVelocity = i.ReadValue<Vector2>();
            _controller.Character.Jump.performed += i => { player.HandleJump(); };
            _controller.Character.Jump.canceled += i => { player.JumpReleased(); };
            _controller.Character.Dash.performed += i => { player.Dash(); };
        }
        _controller.Enable();
    }
    private void OnDisable()
    {
        _controller.Disable();
    }
}