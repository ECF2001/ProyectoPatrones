public interface IPlayerState
{
    void Enter();
    void Exit();
    void Update();
    void FixedUpdate();
    void HandleInput(PlayerStateMachine player); // Método para manejar las entradas
}