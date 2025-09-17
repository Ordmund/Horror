using System.Threading.Tasks;
using Core.Managers;
using Core.Tasks;
using UnityEngine;

namespace World
{
    public class WorldLoadingTask : AsyncTask
    {
        private bool _isCompleted;   
        
        public override async Task Execute()
        {
            await Instantiate();
        }

        private async Task Instantiate()
        {
            var prefab = ResourcesManager.Load<GameObject>("World/World"); //TODO get somewhere path. Should I handle view loading from controller?
            
            var loadingTask = Object.InstantiateAsync(prefab); //TODO where to store the game object? Subscribe on state change to delete?
            var world = await loadingTask;
        }
    }
}