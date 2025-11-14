using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Core.MVC;
using Core.Tasks;

namespace Inventory
{
    public class LoadEquipmentTask : AsyncTask<List<IEquipableItem>>
    {
        private readonly IGameObjectMVCFactory _gameObjectMvcFactory;

        public LoadEquipmentTask(IGameObjectMVCFactory gameObjectMvcFactory)
        {
            _gameObjectMvcFactory = gameObjectMvcFactory;
        }

        public override Task<List<IEquipableItem>> Execute()
        {
            return Load();
        }

        private async Task<List<IEquipableItem>> Load()
        {
            var flashLight = await _gameObjectMvcFactory.InstantiateAndBindAsync<FlashlightController, FlashlightView, EmptyModel>(AddressablesPaths.FlashlightPrefab);

            return new List<IEquipableItem> { flashLight };
        }
    }
}