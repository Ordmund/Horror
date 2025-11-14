using System;

namespace GameStates
{
    public interface IGameStateMachine
    {
        event Action<GameState> OnGameStateChanged;

        GameState CurrentState { get; }

        void ChangeState(GameState state);
    }
}