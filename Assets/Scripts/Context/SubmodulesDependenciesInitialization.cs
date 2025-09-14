using ColoredLogger;
using Constants.Logs;
using Core.Dependencies;

namespace Context
{
    public static class SubmodulesDependenciesInitialization
    {
        public static void Initialize()
        {
            DependenciesProvider.PathHandlerPath = "ScriptableObjects/PathHandler"; 
            
            Logs.Initialize<LogChannel>();
        }
    }
}