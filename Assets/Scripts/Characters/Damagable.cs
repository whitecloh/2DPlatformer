using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagableHit;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _invincibilityTime = 0.25f;
    [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private ShiledAction _shield;

    private HealthBar _healthBar;

    private bool _isAlive = true;
    private bool _isInvincible = false;
    private float _timeSinceHit = 0;

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    public float Health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            if(_currentHealth<=0)
            {
                IsAlive = false;
                _animator.ResetTrigger(AnimationsString.hitTrigger);
            }
            if(_healthBar!=null)
            {
                _healthBar.CurrentFill = Health / MaxHealth;
                _healthBar.HPText.text = string.Format("{0}", Health);
            }
        }
    }
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            _animator.SetBool(AnimationsString.isAlive, value);
        }
    }
    public bool LockVelocity
    {
        get
        {
            return _animator.GetBool(AnimationsString.lockVelocity);
        }
        set
        {
            _animator.SetBool(AnimationsString.lockVelocity, value);
        }
    }

    private void Awake()
    {
        if (GetComponent<PlayerController>())
        {
            _healthBar = FindObjectOfType<HealthBar>();
            _currentHealth = int.Parse(_healthBar.HPText.text);
        }
    }
    private void Update()
    {
        if(_isInvincible)
        {
            if(_timeSinceHit>_invincibilityTime)
            {
                _isInvincible = false;
                _timeSinceHit = 0;
            }
            _timeSinceHit += Time.deltaTime;
        }
    }
    public bool Hit(int damage , Vector2 knockback)
    {
        if (_shield != null && _shield.isActive) return false;

        if(IsAlive && !_isInvincible)
        {
            Health -= damage;
            _isInvincible = true;
            _animator.SetTrigger(AnimationsString.hitTrigger);
            LockVelocity = true;
            damagableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }

    public bool Heal(int hpRestore)
    {
        if(IsAlive&&Health<MaxHealth)
        {
            float maxHeal = Mathf.Max(MaxHealth - Health, 0);
            float actualHeal = Mathf.Min(maxHeal ,hpRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, hpRestore);
            return true;
        }
        return false;
    }

    public void Respawn()
    {
        IsAlive = true;
        Health = MaxHealth;
    }
}
