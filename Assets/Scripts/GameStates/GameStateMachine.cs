using System;
using Core.Managers;
using Zenject;

namespace GameStates
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly IFactory<LoadingTask> _loadingTaskFactory;

        public event Action<GameState> OnGameStateChanged;

        public GameStateMachine(IFactory<LoadingTask> loadingTaskFactory)
        {
            _loadingTaskFactory = loadingTaskFactory;
        }
        
        public GameState CurrentState { get; private set; }

        public void ChangeState(GameState state)
        {
            CurrentState = state;
            
            switch (state)
            {
                case GameState.Preloading:
                    throw new NotImplementedException();
                    //TODO Preload assets for menu
                
                case GameState.Menu:
                    throw new NotImplementedException();
                    //TODO Main menu state
                
                case GameState.Loading:
                    RunLoadingState();
                    break;
                
                case GameState.Game:
                    RunGameState();
                    break;
            }
            
            OnGameStateChanged?.Invoke(CurrentState);
        }

        private void RunLoadingState()
        {
            var loadingTask = _loadingTaskFactory.Create();

            loadingTask.Execute().OnComplete(SwitchToGameState).Forget();
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