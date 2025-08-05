using SABI;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int _speed = 1;
    [SerializeField] private float _rotationSpeed = 1;
    private Vector2 _inputVector = new Vector2(0, 0);
    private bool _isWalking = false;
    private Vector3 _moveDirection;
    
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
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

        _moveDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
        transform.position += _moveDirection * (_speed * Time.deltaTime);

        transform.forward = Vector3.Slerp(transform.forward, new Vector3(_inputVector.x, 0f, _inputVector.y), _rotationSpeed* Time.deltaTime);
        
        _isWalking = _moveDirection != Vector3.zero;
        
        _inputVector =  Vector2.zero;
    }
    
    // [Button]
    // private void DebugFunction()
    // {
    //     Debug.Log(transform.forward);
    // }

    public bool IsWalking()
    {
        return _isWalking;
    }

}
