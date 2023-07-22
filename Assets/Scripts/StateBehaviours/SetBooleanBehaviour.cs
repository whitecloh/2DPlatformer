using UnityEngine;

public class SetBooleanBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private string _boolName;
    [SerializeField]
    private bool updateOnState;
    [SerializeField]
    private bool updateOnStateMashine;
    [SerializeField]
    private bool _valueOnEnter;
    [SerializeField]
    private bool _valueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
            animator.SetBool(_boolName, _valueOnEnter);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
            animator.SetBool(_boolName, _valueOnExit);
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if(updateOnStateMashine)
        animator.SetBool(_boolName, _valueOnEnter);
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMashine)
            animator.SetBool(_boolName, _valueOnExit);
    }
}
