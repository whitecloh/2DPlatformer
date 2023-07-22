using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField]
    private int _hpRestore;
    [SerializeField]
    private AudioClip _clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();

        if (!damagable) return;

        bool wasHealed = damagable.Heal(_hpRestore);
        if (wasHealed)
        {
            AudioSource.PlayClipAtPoint(_clip, transform.position, 1);
            Destroy(gameObject);
        }
    }
}
