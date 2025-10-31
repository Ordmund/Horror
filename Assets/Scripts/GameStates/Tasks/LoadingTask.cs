using System.Threading.Tasks;
using Camera;
using Core.Tasks;
using Player;
using World;
using Zenject;

namespace GameStates
{
    public class LoadingTask : AsyncTask
    {
        private readonly IFactory<LoadWorldTask> _worldLoadingTaskFactory;
        private readonly IFactory<LoadCameraTask> _loadCameraTaskFactory;
        private readonly IFactory<LoadPlayerTask> _playerLoadingTaskFactory;

        public LoadingTask(IFactory<LoadWorldTask> worldLoadingTaskFactory, IFactory<LoadCameraTask> loadCameraTaskFactory, IFactory<LoadPlayerTask> playerLoadingTaskFactory)
        {
            _worldLoadingTaskFactory = worldLoadingTaskFactory;
            _loadCameraTaskFactory = loadCameraTaskFactory;
            _playerLoadingTaskFactory = playerLoadingTaskFactory;
        }

        public override Task Execute()
        {
            return Task.WhenAll(LoadWorld(), LoadCamera(), LoadPlayer());
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