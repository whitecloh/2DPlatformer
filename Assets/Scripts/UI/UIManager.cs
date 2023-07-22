using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _damageTextPrefab;
    [SerializeField]
    private GameObject _healthTextPrefab;
    [SerializeField]
    private ControlPanel _controls;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged+=CharacterTookDamage;
        CharacterEvents.characterHealed+=CharacterHealed;
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged-=CharacterTookDamage;
        CharacterEvents.characterHealed-=CharacterHealed;
    }
    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text textMp = Instantiate(_damageTextPrefab, spawnPosition, Quaternion.identity, _canvas.transform).GetComponent<TMP_Text>();
        textMp.text = damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text textMp = Instantiate(_healthTextPrefab, spawnPosition, Quaternion.identity, _canvas.transform).GetComponent<TMP_Text>();
        textMp.text = healthRestored.ToString();
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _controls.OpenClose();
        }
    }
}
