using System;
using Core.MVC;
using UnityEngine;

namespace Inventory
{
    public class FlashlightView : BaseView
    {
        [SerializeField] private Light _light;
        
        public void Use()
        {
            _light.enabled = !_light.enabled;
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }
        
        public void Unequip()
        {
            throw new NotImplementedException();
        }
    }
}