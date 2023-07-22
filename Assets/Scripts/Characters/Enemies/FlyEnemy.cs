using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField]
    private DetectionZone _biteDetector;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Damagable _damagable;
    [SerializeField]
    private List<Transform> _wayPoints;

    [SerializeField]
    private float _speed;

    private Transform _nextWayPoint;
    private int _wayPointNumber = 0;
    private float _wayPointReachedDistance = 0.1f;

    private bool _hasTarget = false;

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

    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimationsString.canMove);
        }
    }

    private void Start()
    {
        _nextWayPoint = _wayPoints[_wayPointNumber];
    }
    private void Update()
    {
        HasTarget = _biteDetector.DetectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (!_damagable.IsAlive)
        {
            return; 
        }
        if(CanMove)
        {
            Flight();
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    private void Flight()
    {
        Vector2 directionToWayPoint = (_nextWayPoint.position - transform.position).normalized;
        float distance = Vector2.Distance(_nextWayPoint.position, transform.position);

        _rb.velocity = directionToWayPoint * _speed;
        UpdateDirection();
        if(distance<=_wayPointReachedDistance)
        {
            _wayPointNumber++;
            if(_wayPointNumber>=_wayPoints.Count)
            {
                _wayPointNumber = 0;
            }

            _nextWayPoint = _wayPoints[_wayPointNumber];
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        if(transform.localScale.x > 0)
        {
            if(_rb.velocity.x<0)
            {
                transform.localScale = new Vector2(-1*localScale.x, localScale.y);
            }
        }
        else
        {
            if (_rb.velocity.x > 0)
            {
                transform.localScale = new Vector2(-1 * localScale.x, localScale.y);
            }
        }
    }
}
