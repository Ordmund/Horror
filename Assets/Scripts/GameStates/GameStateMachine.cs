using System;
using Core.Managers.Injectable;

namespace GameStates
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly ITaskScheduler _taskScheduler;
        private readonly ITaskFactory _taskFactory;

        public event Action<GameState> OnGameStateChanged;
        
        public GameStateMachine(ITaskScheduler taskScheduler, ITaskFactory taskFactory)
        {
            _taskScheduler = taskScheduler;
            _taskFactory = taskFactory;
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
                    var loadingTask = _taskFactory.InstantiateAndBind<LoadingTask>();
                    
                   _taskScheduler.Run(loadingTask); 
                    break;
                
                case GameState.Game:
                    throw new NotImplementedException();
                    //TODO Gameplay state
            }
            
            OnGameStateChanged?.Invoke(CurrentState);
        }
    }
}