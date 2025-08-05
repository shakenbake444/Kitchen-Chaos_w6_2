using UnityEngine;

public class Inputs : MonoBehaviour
{
    public Vector2 _inputVector = new Vector2(0, 0);
    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
    }
    
    public Vector2 GetMovementVectorNormalized()
    {
        _inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
        
        _inputVector.Normalize();
        return _inputVector;
    }
}
