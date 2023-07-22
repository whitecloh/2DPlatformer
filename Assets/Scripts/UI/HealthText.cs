using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField]
    private RectTransform _textTransform;
    [SerializeField]
    private TextMeshProUGUI _textMP;

    private float _fadeTime = 1f;
    private float _timeElapsed=0;

    private Vector3 _moveSpeed = new Vector3(0, 100, 0);
    private Color _startColor;

    private void Awake()
    {
        _startColor = _textMP.color;
    }
    private void Update()
    {
        _textTransform.position += _moveSpeed * Time.deltaTime;
        _timeElapsed += Time.deltaTime;
        if(_timeElapsed<_fadeTime)
        {
            float fadeAlpha = _startColor.a * (1 - (_timeElapsed / _fadeTime));
            _textMP.color = new Color(_startColor.r, _startColor.g, _startColor.b , fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
