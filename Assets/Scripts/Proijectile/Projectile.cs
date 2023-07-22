using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Vector2 _knockback = new Vector2(0,0);

    private Vector2 _moveSpeed;

    public Vector2 MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
        set
        {
            _moveSpeed = value;
        }
    }

    private void Start()
    {
        _rb.velocity = new Vector2(_moveSpeed.x, _moveSpeed.y);
        StartCoroutine(ProjectileDeath());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        var ground = collision.GetComponent<Ground>();

        if (ground != null)
        {
            Destroy(gameObject);
        }
        if (damagable != null)
        {
            Vector2 knockDirection = transform.localScale.x > 0 ? _knockback : new Vector2(-_knockback.x, _knockback.y);
            bool gotHit = damagable.Hit(_damage, knockDirection);
            if (gotHit)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator ProjectileDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
