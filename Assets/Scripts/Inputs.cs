using System;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public Vector2 _inputVector = new Vector2(0, 0);
    private InputSystem_Actions _inputActions;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        _inputVector= _inputActions.Player.Move.ReadValue<Vector2>();
        
        // if (Input.GetKey(KeyCode.A)) 
        // {
        //     _inputVector.x += -1;
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     _inputVector.x += 1;
        // }
        // if (Input.GetKey(KeyCode.W))
        // {
        //     _inputVector.y += 1;
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     _inputVector.y += -1;
        // }

        _inputVector.Normalize();
        
        return _inputVector;
    }
}
