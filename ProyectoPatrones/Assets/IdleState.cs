using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerStateMachine stateMachine;
    private Animator animator;
    private Rigidbody2D rb;

    public IdleState(PlayerStateMachine stateMachine, Animator animator, Rigidbody2D rb)
    {
        this.stateMachine = stateMachine;
        this.animator = animator;
        this.rb = rb;
    }

    public void Enter()
    {
        // Inicializar par�metros al entrar en el estado idle
        animator.SetFloat("Speed", 0); // Aseg�rate de que "Speed" est� correctamente definido en el Animator
    }

    public void Exit()
    {
        // Limpiar o restablecer valores si es necesario
    }

    public void Update()
    {
        // Aqu� no moveremos al jugador, pero podemos detectar si hay alguna entrada
        HandleInput(stateMachine);
    }

    public void FixedUpdate()
    {
        // No es necesario mover al jugador en el estado Idle
    }

    public void HandleInput(PlayerStateMachine player)
    {
        // Detectar movimiento
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Si el jugador se mueve (en cualquier direcci�n)
        if (horizontalInput != 0 || verticalInput != 0)
        {
            player.ChangeState(new WalkingState(player, player.animator, player.rb)); // Cambiar al estado Walking
        }
    }
}
