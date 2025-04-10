public interface IPlayerState
{
    void Enter();
    void Exit();
    void Update();
    void FixedUpdate();
    void HandleInput(PlayerStateMachine player); // M�todo para manejar las entradas
}