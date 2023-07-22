using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private string _floatName;
    [SerializeField]
    private bool updateOnStateEnter;
    [SerializeField]
    private bool updateOnStateExit;
    [SerializeField]
    private bool updateOnStateMashineEnter;
    [SerializeField]
    private bool updateOnStateMashineExit;
    [SerializeField]
    private float _valueOnEnter;
    [SerializeField]
    private float _valueOnExit;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
            animator.SetFloat(_floatName, _valueOnEnter);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
            animator.SetFloat(_floatName, _valueOnExit);
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMashineEnter)
            animator.SetFloat(_floatName, _valueOnEnter);
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMashineExit)
            animator.SetFloat(_floatName, _valueOnExit);
    }
}
