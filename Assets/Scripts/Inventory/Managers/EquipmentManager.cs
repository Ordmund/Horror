using System;
using System.Collections.Generic;
using System.Linq;
using Core.Managers;
using GameInput;
using GameStates;
using Zenject;

namespace Inventory
{
    public class EquipmentManager : IEquipmentManager, IInitializable, IDisposable, ILoadable
    {
        private readonly IFactory<LoadEquipmentTask> _loadEquipmentTaskFactory;
        private readonly IInputNotifier _inputNotifier;

        private List<IEquipableItem> _availableItems = new();
        private IEquipableItem _leftHandItem;
        private IEquipableItem _rightHandItem;

        public EquipmentManager(IFactory<LoadEquipmentTask> loadEquipmentTaskFactory, IInputNotifier inputNotifier)
        {
            _loadEquipmentTaskFactory = loadEquipmentTaskFactory;
            _inputNotifier = inputNotifier;
        }

        public void Initialize()
        {
            SubscribeOnEvents();
        }
        
        public void Load()
        {
            var task = _loadEquipmentTaskFactory.Create();

            task.OnComplete(InitializePreloadedItems).RunAndForget();
        }

        public void Reload()
        {
            //Ignored
        }

        private void OnEquipmentWheelInteracted()
        {
            OnEquipmentRequested(ItemType.Flashlight);
        }

        private void OnEquipmentRequested(ItemType type)
        {
            var itemToEquip = _availableItems.FirstOrDefault(item => item.Type == type);
            if (itemToEquip != null)
            {
                itemToEquip.Equip();
            }
        }

        private void InitializePreloadedItems(List<IEquipableItem> availableItems)
        {
            _availableItems = availableItems;
        }

        private void SubscribeOnEvents()
        {
            _inputNotifier.EquipmentWheelIsPressed += OnEquipmentWheelInteracted;
        }

        private void UnsubscribeFromEvents()
        {
            _inputNotifier.EquipmentWheelIsPressed -= OnEquipmentWheelInteracted;
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}