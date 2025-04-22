using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public IPlayerState currentState;
    public Rigidbody2D rb;
    public Animator animator; // Nuevo
   // public Faction faction = Faction.Player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Aseg�rate de que el Animator est� en el mismo GameObject
    }

    private void Start()
    {
        currentState = new IdleState(this, rb);
        currentState.Enter();
    }

    private void Update()
    {
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
