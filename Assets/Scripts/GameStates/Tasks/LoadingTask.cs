using System.Collections;
using Core.Managers.Injectable;
using Loading.Tasks;

namespace GameStates
{
    public class LoadingTask : Task
    {
        private readonly ITaskScheduler _taskScheduler;
        private readonly ITaskFactory _taskFactory;

        private bool _isCompeted;

        public LoadingTask(ITaskScheduler taskScheduler, ITaskFactory taskFactory)
        {
            _taskScheduler = taskScheduler;
            _taskFactory = taskFactory;
        }

        public override IEnumerator Execute()
        {
            Load();

            while (!_isCompeted)
            {
                yield return new WaitForNextFrame();
            }
        }

        private void Load()
        {
            var playerLoadingTask = _taskFactory.InstantiateAndBind<PlayerLoadingTask>();
            playerLoadingTask.OnComplete(OnPlayerLoadingCompleted);
            
            _taskScheduler.Run(playerLoadingTask);
        }

        private void OnPlayerLoadingCompleted()
        {
            _isCompeted = true;
        }
    }
}