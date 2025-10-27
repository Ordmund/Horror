using System;
using Core.MVC;
using Zenject;

namespace Cheats
{
    public class CheatsPanelController : BaseController<CheatsPanelView, EmptyModel>, IInitializable, IDisposable
    {
        private readonly IGameObjectMVCFactory _gameObjectMVCFactory;
        
        private CheatCameraSettingsPanelController _cheatCameraSettingsPanelController;
        
        public CheatsPanelController(CheatsPanelView view, EmptyModel model, IGameObjectMVCFactory gameObjectMVCFactory) : base(view, model)
        {
            _gameObjectMVCFactory = gameObjectMVCFactory;
        }

        public void Initialize()
        {
            _cheatCameraSettingsPanelController = _gameObjectMVCFactory.GetComponentAndBind<CheatCameraSettingsPanelController, CheatCameraSettingsPanelView, EmptyModel>(View.gameObject, true);
            
            View.SetActivePanel(CheatsPanelType.CameraSettings);
        }

        public void Dispose()
        {
            _cheatCameraSettingsPanelController = null;
        }
    }
}