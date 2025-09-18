using System.Threading.Tasks;
using Core.Tasks;
using Player;
using World;
using Zenject;

namespace GameStates
{
    public class LoadingTask : AsyncTask
    {
        private readonly IFactory<LoadWorldTask> _worldLoadingTaskFactory;
        private readonly IFactory<LoadPlayerTask> _playerLoadingTaskFactory;

        public LoadingTask(IFactory<LoadWorldTask> worldLoadingTaskFactory, IFactory<LoadPlayerTask> playerLoadingTaskFactory)
        {
            _worldLoadingTaskFactory = worldLoadingTaskFactory;
            _playerLoadingTaskFactory = playerLoadingTaskFactory;
        }

        public override Task Execute()
        {
            return Task.WhenAll(LoadWorld(), LoadPlayer());
        }

        private async Task LoadWorld()
        {
            var worldLoadingTask = _worldLoadingTaskFactory.Create();
            
            await worldLoadingTask.Execute();
        }

        private async Task LoadPlayer()
        {
            var playerLoadingTask = _playerLoadingTaskFactory.Create();
            
            await playerLoadingTask.Execute();
        }
    }
}