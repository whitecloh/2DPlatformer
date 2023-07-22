using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider2D _collider;
    [SerializeField]
    private ContactFilter2D _castFilter;
    [SerializeField]
    private Animator _animator;

    private Vector2 _wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private RaycastHit2D[] _groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] _wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];

    private bool _isGrounded;
    private bool _isOnWall;
    private bool _isOnCeiling;

    private float _wallCheckDistance = 0.4f;
    private float _groundDistance = 0.1f;
    private float _ceilingDistance = 0.2f;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimationsString.isGrounded, value);
        }
    }
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            _animator.SetBool(AnimationsString.isOnWall, value);
        }
    }
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            _animator.SetBool(AnimationsString.isOnCeiling, value);
        }
    }


    private void FixedUpdate()
        {
        IsGrounded = _collider.Cast(Vector2.down, _castFilter,_groundHits,_groundDistance)>0;
        IsOnWall = _collider.Cast(_wallCheckDirection, _castFilter, _wallHits, _wallCheckDistance) > 0;
        IsOnCeiling = _collider.Cast(Vector2.up, _castFilter, _ceilingHits, _ceilingDistance) > 0;
    }
}
