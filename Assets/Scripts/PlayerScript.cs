using SABI;
using UnityEngine;
//initial commit
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int _speed = 1;
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private Inputs _input;
    
    private bool _isWalking = false;
    private Vector3 _moveDirection;
    
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveVector = _input.GetMovementVectorNormalized();
        _moveDirection = new Vector3(moveVector.x, 0, moveVector.y);
        transform.position += _moveDirection * (_speed * Time.deltaTime);

        if (_moveDirection.magnitude > 0.05f)
        {
            transform.forward = Vector3.Slerp(transform.forward, new Vector3(moveVector.x, 0f, moveVector.y), _rotationSpeed* Time.deltaTime);
        }
        
        _isWalking = _moveDirection != Vector3.zero;
        
        _input._inputVector =  Vector2.zero;
    }
    

    public bool IsWalking()
    {
        return _isWalking;
        
    }

}
