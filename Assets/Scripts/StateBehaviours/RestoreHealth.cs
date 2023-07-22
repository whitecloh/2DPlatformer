using UnityEngine;

public class RestoreHealth : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Damagable>().Respawn();
    }
}
