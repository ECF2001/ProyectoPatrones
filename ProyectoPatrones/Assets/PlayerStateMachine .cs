using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public IPlayerState currentState;
    public Rigidbody2D rb;
    public Animator animator;

    public Vector2 movement; // Movimiento compartido

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentState = new IdleState(this, animator, rb);
        currentState.Enter();
    }

    private void Update()
    {
        // Capturamos el input cada frame y lo reseteamos correctamente
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        currentState.HandleInput(this);
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
