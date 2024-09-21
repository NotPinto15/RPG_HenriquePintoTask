using UnityEngine;
using UnityEngine.AI;

public class LoopWalkAnimation : StateMachineBehaviour
{
    private NavMeshAgent agent;

    // Distance to trigger stopping
    [SerializeField] private float stopDistanceThreshold = 0.5f;

    // This method is called when the state machine enters a state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // This method is called every frame while in the state
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null && agent.velocity.magnitude > 0.1f)
        {
            // Continue playing the walk animation
            animator.Play("Walk");
        }
        else if (agent.remainingDistance <= stopDistanceThreshold)
        {
            // Trigger transition to idle animation
            animator.SetTrigger("ToIdle");
        }
    }
}