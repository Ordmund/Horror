using UnityEngine;
using Zenject;

namespace Context
{
    public class SceneContextRunner : MonoBehaviour
    {
        [SerializeField] private SceneContext sceneContext;
        
        private void Start()
        {
            SubmodulesDependenciesInitialization.Initialize();
            
            sceneContext.Run();
        }
    }
}