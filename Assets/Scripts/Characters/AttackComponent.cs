using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private Vector2 _knockback = Vector2.zero;

    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();

        if (damagable == null) return;

        Vector2 knockDirection = transform.parent.localScale.x > 0 ? _knockback : new Vector2(-_knockback.x, _knockback.y);
        bool gotHit = damagable.Hit(_damage, knockDirection);
    }
}
