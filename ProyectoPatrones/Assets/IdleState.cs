using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerStateMachine stateMachine;
    private Rigidbody2D rb;
    private Animator animator;

    public IdleState(PlayerStateMachine stateMachine, Animator animator, Rigidbody2D rb)
    {
        this.stateMachine = stateMachine;
        this.animator = animator;
        this.rb = rb;
    }

    public void Enter()
    {
        animator.SetBool("isWalking", false);
        animator.SetFloat("Speed", 0f);
    }

    public void Exit() { }

    public void Update()
    {
        // Si hay input, cambiamos a WalkingState
        if (stateMachine.movement.sqrMagnitude > 0.01f)
        {
            stateMachine.ChangeState(new WalkingState(stateMachine, animator, rb));
        }
    }

    public void FixedUpdate() { }

    public void HandleInput(PlayerStateMachine player) { }
}
