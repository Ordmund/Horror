using System;
using GameInput;
using GameStates;
using Zenject;

namespace Context
{
    public class GameContext : IInitializable, IDisposable
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IFactory<InputController> _inputControllerFactory;

        private InputController _inputController;

        public GameContext(IGameStateMachine gameStateMachine, IFactory<InputController> inputControllerFactory)
        {
            _gameStateMachine = gameStateMachine;
            _inputControllerFactory = inputControllerFactory;
        }

        public void Initialize()
        {
            _inputController = _inputControllerFactory.Create();
            
            _gameStateMachine.ChangeState(GameState.Loading);
        }
        
        public void Dispose()
        {
            _inputController?.Dispose();
        }
    }
}