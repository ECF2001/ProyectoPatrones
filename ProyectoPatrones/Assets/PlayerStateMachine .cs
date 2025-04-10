using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public IPlayerState currentState;
    public Animator animator;
    public Rigidbody2D rb;

    private void Start()
    {
        currentState = new IdleState(this, animator, rb); // El estado inicial es Idle
        currentState.Enter();
    }

    private void Update()
    { 
        currentState.HandleInput(this); // Maneja la entrada del jugador
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState.Exit(); // Salir del estado actual
        currentState = newState;
        currentState.Enter(); // Entrar en el nuevo estado
    }
}
