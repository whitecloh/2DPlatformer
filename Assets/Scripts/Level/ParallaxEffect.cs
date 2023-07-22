using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    public Transform _followTarget;

    private Vector2 _startingPosition;

    private float _startingZ;

    private Vector2 camMoveSinceStart => (Vector2)_camera.transform.position-_startingPosition;
    private float zDistanceFromTarget => transform.position.z - _followTarget.position.z;
    private float clippingPlane => (_camera.transform.position.z + (zDistanceFromTarget > 0 ? _camera.farClipPlane : _camera.nearClipPlane));

    private float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    private void Awake()
    {
        _followTarget = PlayerController.Instance.transform;
    }
    private void Start()
    {
        _startingPosition = transform.position;
        _startingZ = transform.position.z;
    }
    private void Update()
    {
        Vector2 newPosition = _startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, _startingZ); 
    }

    public void UpdateFollower()
    {
        _followTarget = PlayerController.Instance.transform;
    }
}
