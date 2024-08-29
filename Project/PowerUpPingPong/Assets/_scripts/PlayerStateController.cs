public enum PlayerStates
{
    idle,
    moving,
    attacking,
    dead
}

public class PlayerStateController : StateMachineBase<PlayerStates>
{
    protected override void UpdateState()
    {
        switch(_currentState)
        {
            case PlayerStates.idle:
                break;
            case PlayerStates.moving:
                break;
            case PlayerStates.attacking:
                break;
            case PlayerStates.dead:
                break;
        }
    }
}
