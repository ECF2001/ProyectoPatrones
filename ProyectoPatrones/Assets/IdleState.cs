using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerStateMachine stateMachine;
    private Rigidbody2D rb;

    public IdleState(PlayerStateMachine stateMachine, Rigidbody2D rb)
    {
        this.stateMachine = stateMachine;
        this.rb = rb;
    }

    public void Enter()
    {
        stateMachine.animator.SetBool("isWalking", false);
    }

    public void Exit() { }

    public void Update()
    {
        HandleInput(stateMachine);
    }

    public void FixedUpdate() { }

    public void HandleInput(PlayerStateMachine player)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            player.ChangeState(new WalkingState(player, player.rb));
        }
    }
}
