using System;
using ColoredLogger;
using Constants.Logs;
using Controllers.InputControllers;
using World;
using Zenject;

namespace Context
{
    public class GameContext : IGameContext, IInitializable, IDisposable
    {
        private readonly IFactory<InputController> _inputControllerFactory;
        private readonly IWorldBuilder _worldBuilder;
        
        private InputController _inputController;

        public GameContext(IFactory<InputController> inputControllerFactory, IWorldBuilder worldBuilder)
        {
            _inputControllerFactory = inputControllerFactory;
            _worldBuilder = worldBuilder;
        }

        public void Initialize()
        {
            Logs.Initialize<LogChannel>();
            
            _inputController = _inputControllerFactory.Create();

            _worldBuilder.BuildWorld();
        }

        public void Dispose()
        {
            _inputController?.Dispose();
        }
    }
}