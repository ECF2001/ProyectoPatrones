using UnityEngine;

public class WalkingState : IPlayerState
{
    private PlayerStateMachine stateMachine;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float moveSpeed = 5f;

    public WalkingState(PlayerStateMachine stateMachine, Animator animator, Rigidbody2D rb)
    {
        this.stateMachine = stateMachine;
        this.animator = animator;
        this.rb = rb;
    }

    public void Enter()
    {
        // Configurar parámetros al entrar al estado de caminar
        animator.SetFloat("Speed", 1); // Asegúrate de que "Speed" esté correctamente definido en el Animator
    }

    public void Exit()
    {
        // Limpiar valores si es necesario
    }

    public void Update()
    {
        // Capturar entrada de movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Actualizar los parámetros del Animator
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);  // Activar la animación de caminar

        // Si el jugador deja de moverse, cambiar al estado Idle
        if (movement.sqrMagnitude == 0)
        {
            stateMachine.ChangeState(new IdleState(stateMachine, animator, rb));
        }

        Debug.Log("Horizontal: " + Input.GetAxisRaw("Horizontal"));
        Debug.Log("Vertical: " + Input.GetAxisRaw("Vertical"));
    }

    public void FixedUpdate()
    {
        // Mover al jugador según la entrada
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void HandleInput(PlayerStateMachine player)
    {
        // Este método no es necesario aquí, ya que HandleInput se maneja dentro de Update
    }
}
