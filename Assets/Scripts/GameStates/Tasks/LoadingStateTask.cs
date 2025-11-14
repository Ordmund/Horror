using System.Collections.Generic;
using System.Threading.Tasks;
using Camera;
using Core.Tasks;
using Player;
using World;
using Zenject;

namespace GameStates
{
    public class LoadingStateTask : AsyncTask
    {
        private readonly IFactory<LoadWorldTask> _worldLoadingTaskFactory;
        private readonly IFactory<LoadCameraTask> _loadCameraTaskFactory;
        private readonly IFactory<LoadPlayerTask> _playerLoadingTaskFactory;
        private readonly List<ILoadable> _loadables;

        public LoadingStateTask(IFactory<LoadWorldTask> worldLoadingTaskFactory, IFactory<LoadCameraTask> loadCameraTaskFactory, IFactory<LoadPlayerTask> playerLoadingTaskFactory,
            List<ILoadable> loadables)
        {
            _worldLoadingTaskFactory = worldLoadingTaskFactory;
            _loadCameraTaskFactory = loadCameraTaskFactory;
            _playerLoadingTaskFactory = playerLoadingTaskFactory;
            _loadables = loadables;
        }

        public override Task Execute()
        {
            return Load();
        }

        private async Task Load()
        {
            await Task.WhenAll(LoadWorld(), LoadCamera(), LoadPlayer());

            foreach (var loadable in _loadables)
            {
                loadable.Load();
            }
        }

        private async Task LoadWorld()
        {
            var worldLoadingTask = _worldLoadingTaskFactory.Create();

            await worldLoadingTask.Execute();
        }

        private async Task LoadCamera()
        {
            var cameraLoadingTask = _loadCameraTaskFactory.Create();

            await cameraLoadingTask.Execute();
        }

        private async Task LoadPlayer()
        {
            var playerLoadingTask = _playerLoadingTaskFactory.Create();

            await playerLoadingTask.Execute();
        }
    }
}