using Core.MVC;
using UnityEngine;

namespace Player.ScriptableObjects
{
    public class PlayerSettings : ScriptableObject, IModelLoader<PlayerModel>
    {
        [SerializeField] private PlayerModel model;
        
        public PlayerModel LoadModel()
        {
            return model;
        }
    }
}