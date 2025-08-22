using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public Vector2 _inputVector = new Vector2(0, 0);
    public event EventHandler OnInteractAction;
    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        Debug.Log(obj); 
        //OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    
    public Vector2 GetMovementVectorNormalized()
    {
        _inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
        
        _inputVector.Normalize();
        return _inputVector;
    }
}
