using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;
    [SerializeField] private Transform _counterTopPoint;
    private Transform _objectOnCounterTop;
    
    
    public void Interact()
    {
        Debug.Log("Interact!");
        if (_objectOnCounterTop == null)
        {
            _objectOnCounterTop = Instantiate(_kitchenObjectSo.prefab, _counterTopPoint.transform.position, Quaternion.identity);
        }
    }
}
