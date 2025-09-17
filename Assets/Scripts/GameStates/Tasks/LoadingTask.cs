using System.Threading.Tasks;
using Core.Tasks;
using Player;
using World;
using Zenject;

namespace GameStates
{
    public class LoadingTask : AsyncTask
    {
        private readonly IFactory<WorldLoadingTask> _worldLoadingTaskFactory;
        private readonly IFactory<PlayerLoadingTask> _playerLoadingTaskFactory;

        public LoadingTask(IFactory<WorldLoadingTask> worldLoadingTaskFactory, IFactory<PlayerLoadingTask> playerLoadingTaskFactory)
        {
            _worldLoadingTaskFactory = worldLoadingTaskFactory;
            _playerLoadingTaskFactory = playerLoadingTaskFactory;
        }

        public override async Task Execute()
        {
            await LoadWorld();
            await LoadPlayer();
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