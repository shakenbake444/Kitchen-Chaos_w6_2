using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private ClearCounter _clearCounter;
    [SerializeField] private GameObject _selectedVisual;
    private void Start()
    {
        PlayerScript.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerScript.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == _clearCounter)
        {
            ShowGameObject();
            //_clearCounter.Interact();
        }
        else
        {
            HideGameObject();
        }
    }

    private void ShowGameObject()
    {
        _selectedVisual.SetActive(true);
    }

    private void HideGameObject()
    {
        _selectedVisual.SetActive(false);
    }
}
