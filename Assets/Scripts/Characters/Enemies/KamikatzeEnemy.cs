using UnityEngine;

public class KamikatzeEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private DetectionZone _explosionDetector , _playerDetector;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private TouchingDirections _touchingDirections;
    [SerializeField]
    private Damagable _damagable;
    [SerializeField]
    private DetectionZone _cliffDetector;
    [SerializeField]
    private float _walkStopRate;
    [SerializeField]
    private float _speed;

    private bool _hasTarget = false;
    private WalkableDirection _direction;
    private Vector2 _directionVector = Vector2.right;

    public WalkableDirection Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (_direction != value)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    _directionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    _directionVector = Vector2.left;
                }
            }
            _direction = value;
        }
    }
    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimationsString.canMove);
        }
    }
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationsString.hasTarget, value);
        }
    }

    private void Update()
    {
        HasTarget = _explosionDetector.DetectedColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if (_touchingDirections.IsGrounded && _touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (_damagable.LockVelocity)
        {
            _rb.gravityScale = 100;
            return; 
        }

        _rb.gravityScale = 1;
        if (CanMove && _touchingDirections.IsGrounded)
        {
            _rb.velocity = new Vector2(_speed * _directionVector.x, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, _walkStopRate), _rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (Direction == WalkableDirection.Right)
        {
            Direction = WalkableDirection.Left;
        }
        else if (Direction == WalkableDirection.Left)
        {
            Direction = WalkableDirection.Right;
        }
        else
        {

        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        _rb.velocity = new Vector2(knockback.x, _rb.velocity.y + knockback.y);
        if (_playerDetector.DetectedColliders.Count < 1)
        {
            FlipDirection();
        }
    }

    public void OnClifDetected()
    {
        if (_touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
