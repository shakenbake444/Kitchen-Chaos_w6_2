using System;
using SABI;
using UnityEngine;
//initial commit
public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    
    [SerializeField] private int _speed = 1;
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private Inputs _input;
    [SerializeField] private LayerMask countersLayerMask;
    
    private bool _isWalking = false;
    private Vector3 _moveDirection;
    private Vector3 _lastInteractDirection;
    private ClearCounter _selectedCounter;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.Log("More than one PlayerScript");
        }
        Instance = this;
    }
    private void Start()
    {
        _input.OnInteractAction += _Input_OnInteractAction;

    }

    private void _Input_OnInteractAction(object sender, System.EventArgs e)
    {
        if (_selectedCounter != null)
        {
            //_selectedCounter.Interact();
        }
        
        Vector2 inputVector = _input.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            //_lastInteractDirection = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //clearCounter.Interact();
                //Has ClearCounter
                clearCounter.Interact();

            }
        }
    }
    
    
    private void Update()
    {
        MovePlayer();
        HandleInteractions();
    }

    private void MovePlayer()
    {
        Vector2 moveVector = _input.GetMovementVectorNormalized();
        _moveDirection = new Vector3(moveVector.x, 0, moveVector.y);

        float moveDistance = _speed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(point1: transform.position + Vector3.up, point2: transform.position + Vector3.up * playerHeight, radius: playerRadius, direction: _moveDirection, maxDistance: moveDistance);

        if (!canMove)
        {
            //cannot move towards moveDir
            
            //Attempt only X movement
            Vector3 moveDirX = new Vector3(_moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(point1: transform.position, point2: transform.position + Vector3.up * playerHeight, radius: playerRadius, direction: moveDirX, maxDistance: moveDistance);
            
            if (canMove)
            {
                _moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, _moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(point1: transform.position, point2: transform.position + Vector3.up * playerHeight, radius: playerRadius, direction: moveDirZ, maxDistance: moveDistance);

                if (canMove)
                {
                    _moveDirection = moveDirZ;
                }
                else
                {
                    //cannot move in any direction    
                }
            }
        }
        
        if (canMove)
        {
            transform.position += _moveDirection * (_speed * Time.deltaTime);
        }
        
        if (_moveDirection.magnitude > 0.05f)
        {
            transform.forward = Vector3.Slerp(transform.forward, new Vector3(moveVector.x, 0f, moveVector.y), _rotationSpeed* Time.deltaTime);
        }
        
        _isWalking = _moveDirection != Vector3.zero;
        
        _input._inputVector =  Vector2.zero;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = _input.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractDirection = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //clearCounter.Interact();
                //Has ClearCounter
                if (clearCounter != _selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    public bool IsWalking()
    {
        return _isWalking;
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this._selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs() { selectedCounter = _selectedCounter });
    }
}
