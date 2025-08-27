using Core.MVC;

namespace Providers
{
    public interface IPrefabPathProvider
    {
        string GetPathByViewType<T>() where T : BaseView;
    }
}