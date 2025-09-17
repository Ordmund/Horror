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
                    SwitchToLoadingState();
                    break;
                
                case GameState.Game:
                    throw new NotImplementedException();
                    //TODO Gameplay state
            }
            
            OnGameStateChanged?.Invoke(CurrentState);
        }

        private void SwitchToLoadingState()
        {
            var loadingTask = _loadingTaskFactory.Create();

            loadingTask.Execute().Forget(); //TODO still need onComplete method
        }
    }
}