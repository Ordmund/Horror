using System.Threading.Tasks;
using Constants;
using Core.Tasks;
using UnityEngine.AddressableAssets;

namespace World
{
    public class LoadWorldTask : AsyncTask
    {
        private bool _isCompleted;

        public override Task Execute()
        {
            return Instantiate();
        }

        private async Task Instantiate()
        {
            var instantiateAssetTask = Addressables.InstantiateAsync(AddressablesPaths.WorldPrefab);

            await instantiateAssetTask.Task;

            _ = instantiateAssetTask.Result;
        }
    }
}