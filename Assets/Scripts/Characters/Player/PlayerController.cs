using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private TouchingDirections _touchingDirections;
    [SerializeField]
    private float _jumpImpulse;
    [SerializeField]
    private float _doubleJumpImpulse;
    [SerializeField]
    private float _inAirSpeed;
    [SerializeField]
    private Damagable _damagable;
    [SerializeField]
    private AttackComponent _attackComponent;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _dashForce;

    private CinemachineVirtualCamera _cinemachine;
    private Vector2 moveInput;
    private int _damage;
    private bool IsDoubleJump;
    private bool isMoving = false;
    private bool isFacingRight=true;

    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
        private set
        {
            if(isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            isFacingRight = value;
        }
    }
    public bool IsMoving {
        get
        {
            return isMoving;
        }
        private set
        {
            isMoving = value;
            _animator.SetBool(AnimationsString.isMoving, isMoving);
        }

    }
    public float CurrentMoveSpeed
    {
        get
        {
            if (!CanMove) return 0;

            if(IsMoving&&!_touchingDirections.IsOnWall)
            {
                if (_touchingDirections.IsGrounded)
                    return _speed;
                else
                    return _inAirSpeed;
            }
            else
            {
                return 0;
            }
        }
    }

    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimationsString.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return _animator.GetBool(AnimationsString.isAlive);
        }
    }

    public bool IsRobotActionActive { get; set; }

    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        _cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachine.Follow = this.transform;
        _damage = _attackComponent.Damage;
    }
    private void FixedUpdate()
    {
        if(!_damagable.LockVelocity)
        {
            _rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, _rb.velocity.y);
        }
        _animator.SetFloat(AnimationsString.yVelocity,_rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsAlive) { IsMoving = false; return; }

        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput.x != 0;
        SetFacingDirection(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!CanMove) return;

        if(context.started && _touchingDirections.IsGrounded)
        {
            IsDoubleJump = true;
            _animator.SetTrigger(AnimationsString.jumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpImpulse);
        }
        else if(context.started&&IsDoubleJump)
        {
            _animator.SetTrigger(AnimationsString.jumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, _doubleJumpImpulse);
            IsDoubleJump = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _animator.SetTrigger(AnimationsString.attackTrigger);
            _attackComponent.Damage = _damage;
        }
    }

    public void OnStrongAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _attackComponent.Damage = _damage * 2;
            _animator.SetTrigger(AnimationsString.strongAttackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started && _animator.GetBool("isGrounded"))
        {
            _animator.SetTrigger(AnimationsString.rangeTrigger);
        }
    }

    public void OnHit(int damage,Vector2 knockback)
    {
        _rb.velocity = new Vector2(knockback.x, _rb.velocity.y + knockback.y);
    }

    public void OnDash()
    {
        if(IsFacingRight)
        {
            _rb.AddForce(Vector2.right * _dashForce);
        }
        else
        {
            _rb.AddForce(Vector2.left * _dashForce);
        }
    }

    public void OnChangeSkin(InputAction.CallbackContext context)
    {
        if (context.started&&IsRobotActionActive)
        {
            SkinChanger.Instance.ChangeSkin();
        }
    }
    public void OnDeath()
    {
        SceneManager.LoadScene("LoseScene");
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

}
