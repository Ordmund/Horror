using System;
using Core.Managers;
using Zenject;

namespace GameStates
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly IFactory<LoadingStateTask> _loadingStateTaskFactory;

        public event Action<GameState> OnGameStateChanged;

        public GameStateMachine(IFactory<LoadingStateTask> loadingStateTaskFactory)
        {
            _loadingStateTaskFactory = loadingStateTaskFactory;
        }

        public GameState CurrentState { get; private set; }

        public void ChangeState(GameState state)
        {
            CurrentState = state;
            
            switch (state)
            {
                case GameState.Launching:
                    throw new NotImplementedException();
                    //TODO Preload assets for menu
                
                case GameState.Menu:
                    throw new NotImplementedException();
                    //TODO Main menu state
                
                case GameState.Loading:
                    RunLoadingState();
                    break;
                
                case GameState.Reloading:
                    RunReloadingState();
                    break;
                
                case GameState.Game:
                    RunGameState();
                    break;
            }
            
            OnGameStateChanged?.Invoke(CurrentState);
        }

        private void RunLoadingState()
        {
            var loadingTask = _loadingStateTaskFactory.Create();

            loadingTask.OnComplete(SwitchToGameState).RunAndForget();
        }

        private void RunReloadingState()
        {
            SwitchToGameState();
        }

        private void RunGameState()
        {
            //TODO Gameplay state
        }

        private void SwitchToGameState()
        {
            ChangeState(GameState.Game);
        }
    }
}