using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private Transform _spawnPosition;


    public void FireProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab,_spawnPosition.position,_projectilePrefab.transform.rotation);
        if(!_spawnPosition.parent.GetComponent<PlayerController>())
        {
            projectile.GetComponent<Projectile>().MoveSpeed = PlayerController.Instance.transform.position - _spawnPosition.position;
        }
        else
        {
            projectile.GetComponent<Projectile>().MoveSpeed = new Vector2(15*_spawnPosition.parent.localScale.x,0);
        }

            Vector3 originalScale = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(originalScale.x * (transform.localScale.x > 0 ? 1 : -1), originalScale.y, originalScale.z);
    }
}
