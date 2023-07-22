using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _healthBar;
    [SerializeField]
    private Text _hpText;

    private float _currentFill = 1;

    public Text HPText => _hpText;
    public float CurrentFill {
        get
        {
            return _currentFill;
        }
        set
        {
            _currentFill = value;
        }
    }

    private void Update()
    {
        if (CurrentFill != _healthBar.fillAmount)
        {
            _healthBar.fillAmount = Mathf.MoveTowards(_healthBar.fillAmount, CurrentFill, Time.deltaTime * 1);
        }
        _healthBar.fillAmount = CurrentFill;
    }

}
