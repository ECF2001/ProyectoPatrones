using UnityEngine;

public class WalkingState : IPlayerState
{
    private PlayerStateMachine stateMachine;
    private Animator animator;
    private Rigidbody2D rb;
    private float moveSpeed = 5f;

    public WalkingState(PlayerStateMachine stateMachine, Animator animator, Rigidbody2D rb)
    {
        this.stateMachine = stateMachine;
        this.animator = animator;
        this.rb = rb;
    }

    public void Enter()
    {
        animator.SetBool("isWalking", true);
    }

    public void Exit()
    {
        animator.SetBool("isWalking", false);
    }

    public void Update()
    {
        // Actualizamos animaciones con el input actual
        animator.SetFloat("Horizontal", stateMachine.movement.x);
        animator.SetFloat("Vertical", stateMachine.movement.y);
        animator.SetFloat("Speed", stateMachine.movement.sqrMagnitude);

        // Si deja de moverse, volvemos a Idle
        if (stateMachine.movement.sqrMagnitude == 0)
        {
            stateMachine.ChangeState(new IdleState(stateMachine, animator, rb));
        }
    }

    public void FixedUpdate()
    {
        if (stateMachine.movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + stateMachine.movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void HandleInput(PlayerStateMachine player) { }
}
