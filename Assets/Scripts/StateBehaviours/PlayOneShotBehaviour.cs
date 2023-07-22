using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private AudioClip _clip;
    [SerializeField]
    private float _volume;
    [SerializeField]
    private bool _playOnEnter = true, _playOnExit=false, _playAfterDelay=false;
    [SerializeField]
    private float _playDelay = 0.25f;

    private float _timeSinceEnter = 0;
    private bool hasDelayedSoundPlayer = false;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_playOnEnter)
        {
            AudioSource.PlayClipAtPoint(_clip, animator.gameObject.transform.position, _volume);
        }
      _timeSinceEnter = 0;
      hasDelayedSoundPlayer = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_playAfterDelay&&!hasDelayedSoundPlayer)
        {
            _timeSinceEnter += Time.deltaTime;

            if(_timeSinceEnter>_playDelay)
            {
                AudioSource.PlayClipAtPoint(_clip, animator.gameObject.transform.position, _volume);
                hasDelayedSoundPlayer = true;
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playOnExit)
        {
            AudioSource.PlayClipAtPoint(_clip, animator.gameObject.transform.position, _volume);
        }
    }
}
