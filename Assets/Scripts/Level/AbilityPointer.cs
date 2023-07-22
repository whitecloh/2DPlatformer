using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPointer : MonoBehaviour
{
    [SerializeField]
    private Text _messageWindow;
    [SerializeField]
    private string _abilityName;

    private string _message;
    private void Awake()
    {
        _messageWindow.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player == null) return;

        if (_abilityName == "Shield")
        {
            player.GetComponent<ShiledAction>().IsShieldActionActive = true;
            _message = string.Format("New Ability\nShield\nPress X");
        }
        if (_abilityName == "Robot")
        {
            player.IsRobotActionActive = true;
            _message = string.Format("New Ability\nRobot\nPress Z");
        }
        StartCoroutine(Message());
    }

    private IEnumerator Message()
    {
        _messageWindow.enabled = true;
        _messageWindow.text = _message;
        yield return new WaitForSeconds(4f);
        _messageWindow.enabled = false;
        _messageWindow.text = string.Empty;
        Destroy(gameObject);
    }
}
