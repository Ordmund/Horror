using Core.Managers;
using Zenject;

namespace Cheats.Cheats
{
    public class CheatsContext : IInitializable
    {
        private readonly IFactory<LoadCheatsPanelTask> _loadCheatsPanelTaskFactory;

        public CheatsContext(IFactory<LoadCheatsPanelTask> loadCheatsPanelTaskFactory)
        {
            _loadCheatsPanelTaskFactory = loadCheatsPanelTaskFactory;
        }

        public void Initialize()
        {
            var task = _loadCheatsPanelTaskFactory.Create();
            
            task.RunAndForget();
        }
    }
}