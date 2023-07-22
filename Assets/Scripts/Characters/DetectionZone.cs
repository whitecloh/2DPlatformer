using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;
    [SerializeField]
    private Collider2D _collider;

    private List<Collider2D> detectedColliders = new List<Collider2D>();
    public List<Collider2D> DetectedColliders => detectedColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
        if(detectedColliders.Count<=0)
        {
            noCollidersRemain.Invoke();
        }
    }

}
