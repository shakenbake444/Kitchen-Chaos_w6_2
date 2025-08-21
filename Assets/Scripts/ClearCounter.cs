using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform _tomatoPrefab;
    [SerializeField] private Transform _counterTopPoint;
    private Transform _objectOnCounterTop;
    
    
    public void Interact()
    {
        Debug.Log(_objectOnCounterTop == null);
        if (_objectOnCounterTop == null)
        {
            _objectOnCounterTop = Instantiate(_tomatoPrefab, _counterTopPoint.transform.position, Quaternion.identity);
        }
    }
}
