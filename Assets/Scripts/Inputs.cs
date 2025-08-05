using UnityEngine;

public class Inputs : MonoBehaviour
{
    public Vector2 _inputVector = new Vector2(0, 0);
    
    public Vector2 GetMovementVectorNormalized()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            _inputVector.x += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _inputVector.x += 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            _inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _inputVector.y += -1;
        }
        
        _inputVector.Normalize();
        return _inputVector;
    }
}
