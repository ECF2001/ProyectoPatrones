using UnityEngine;

public class WalkingState : IPlayerState
{
    private PlayerStateMachine stateMachine;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float moveSpeed = 5f;

    public WalkingState(PlayerStateMachine stateMachine, Rigidbody2D rb)
    {
        this.stateMachine = stateMachine;
        this.rb = rb;
    }

    public void Enter()
    {
        stateMachine.animator.SetBool("isWalking", true);
    }

    public void Exit() { }

    public void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude == 0)
        {
            stateMachine.ChangeState(new IdleState(stateMachine, rb));
        }
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void HandleInput(PlayerStateMachine player) { }
}
