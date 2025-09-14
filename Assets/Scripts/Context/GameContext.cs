using System;
using Characters.Player;
using ColoredLogger;
using Constants.Logs;
using Controllers.InputControllers;
using Core.Dependencies;
using Core.Managers.Injectable;
using Core.MVC;
using Zenject;

namespace Context
{
    public class GameContext : IGameContext, IInitializable, IDisposable
    {
        private readonly IFactory<InputController> _inputControllerFactory;
        private readonly IGameObjectMVCFactory _gameObjectMvcFactory;
        private readonly ITaskScheduler _taskScheduler;

        private InputController _inputController;

        public GameContext(IFactory<InputController> inputControllerFactory, IGameObjectMVCFactory gameObjectMvcFactory, ITaskScheduler taskScheduler)
        {
            _inputControllerFactory = inputControllerFactory;
            _gameObjectMvcFactory = gameObjectMvcFactory;
            _taskScheduler = taskScheduler;
        }

        public void Initialize()
        {
            Logs.Initialize<LogChannel>();

            DependenciesProvider.PathHandlerPath = "ScriptableObjects/PathHandler";

            _inputController = _inputControllerFactory.Create();

            _taskScheduler.Run(new ActionTask(LoadCharacter)); //TODO why gamecontext? Context -> Menu -> LoadWorld -> LoadCharacter.
        }
        
        private void LoadCharacter()
        {
            var playerController = _gameObjectMvcFactory.InstantiateAndBind<PlayerController, PlayerView, PlayerModel>(); //TODO where to store? (somewhere, where it will be disposed)
        }

        public void Dispose()
        {
            _inputController?.Dispose();
        }
    }
}