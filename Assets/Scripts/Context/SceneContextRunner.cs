using UnityEngine;
using Zenject;

namespace Context
{
    public class SceneContextRunner : MonoBehaviour
    {
        [SerializeField] private SceneContext _sceneContext;
        
        private void Start()
        {
            SubmodulesDependenciesInitialization.Initialize();
            
            _sceneContext.Run();
        }
    }
}