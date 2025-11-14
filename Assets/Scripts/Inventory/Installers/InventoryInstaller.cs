using Zenject;

namespace Inventory
{
    public class InventoryInstaller : Installer<InventoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<LoadEquipmentTask>().AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EquipmentManager>().AsSingle();
        }
    }
}