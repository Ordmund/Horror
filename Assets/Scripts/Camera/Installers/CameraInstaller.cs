using Zenject;

namespace Camera
{
    public class CameraInstaller : Installer<CameraInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<LoadCameraTask>().AsSingle();
            Container.Bind<ICameraSwitchDispatcher>().To<CameraSwitchDispatcher>().AsSingle();
        }
    }
}