using System.Collections;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject _skin1, _skin2;
    [SerializeField]
    private float lifeTime;

    private ParallaxEffect[] _parallax;

    private GameObject currentSkin;
    private Vector3 currentPosition;

    private bool isRobotActive;
    private bool isShieldActive;

    private static SkinChanger instance;

    public static SkinChanger Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SkinChanger>();
            }
            return instance;
        } 
    }

    private void Awake()
    {
        _parallax = FindObjectsOfType<ParallaxEffect>();
        currentPosition = transform.position;
        LoadDefaultSkin(currentPosition);
    }

    public void ChangeSkin()
    {
        StartCoroutine(Change(currentSkin.transform.position));
    }

    private IEnumerator Change(Vector3 position)
    {
        DestroyCurrentSkin();
        LoadRobotSkin(position);
        yield return new WaitForSeconds(lifeTime);
        currentPosition = currentSkin.transform.position;
        DestroyCurrentSkin();
        LoadDefaultSkin(currentPosition);
    }

    private void LoadDefaultSkin(Vector3 position)
    {
        currentSkin = Instantiate(_skin1, transform);
        currentSkin.transform.position = position;
        currentSkin.GetComponent<PlayerController>().IsRobotActionActive = isRobotActive;
        currentSkin.GetComponent<ShiledAction>().IsShieldActionActive = isShieldActive;
        SetTargetParallax();
    }
    private void LoadRobotSkin(Vector3 position)
    {
        currentSkin = Instantiate(_skin2, transform);
        currentSkin.transform.position = position;
        currentSkin.GetComponent<PlayerController>().IsRobotActionActive = isRobotActive;
        currentSkin.GetComponent<ShiledAction>().IsShieldActionActive = isShieldActive;
        SetTargetParallax();
    }
    private void DestroyCurrentSkin()
    {
        isRobotActive = currentSkin.GetComponent<PlayerController>().IsRobotActionActive;
        isShieldActive = currentSkin.GetComponent<ShiledAction>().IsShieldActionActive;
        Destroy(currentSkin);
    }

    private void SetTargetParallax()
    {
        foreach (var paralax in _parallax)
        {
            paralax._followTarget = currentSkin.transform;
        }
    }
}
