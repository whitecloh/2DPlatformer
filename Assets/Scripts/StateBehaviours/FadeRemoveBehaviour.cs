using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float _fadeTime;
    [SerializeField]
    private float _fadeDelay;

    private float _fadeDelayElapsed=0;
    private float _timeElapsed;

    private SpriteRenderer _spriteRenderer;
    private GameObject _objectToRemove;
    private Color _startColor;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed = 0f;
        _spriteRenderer = animator.GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
        _objectToRemove = animator.gameObject;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (_fadeDelay > _fadeDelayElapsed)
        {
            _fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            _timeElapsed += Time.deltaTime;
            float newAlpha = _startColor.a * (1 - _timeElapsed / _fadeTime);

            _spriteRenderer.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);
            if (_timeElapsed > _fadeTime)
            {
                Destroy(_objectToRemove);
            }
        }
    }
}
