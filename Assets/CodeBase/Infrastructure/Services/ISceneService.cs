using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services
{
    public interface ISceneService : IService
    {
        SceneStaticData CurrentScene { get; }

        void SetFirstScene();
        void SetCurrentScene(string sceneName);
        SceneStaticData GetNextScene();
    }
}