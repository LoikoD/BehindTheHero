using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class SceneService : ISceneService
    {
        private readonly SceneListStaticData _scenes;
        private SceneStaticData _currentScene;

        public SceneService(IStaticDataService staticDataService)
        {
            _scenes = staticDataService.AllScenes();
        }

        public SceneStaticData CurrentScene => _currentScene;

        public void SetFirstScene()
        {
            _currentScene = _scenes.ScenesInOrder[0];
        }

        public void SetCurrentScene(string sceneName)
        {
            SceneStaticData scene = _scenes.ScenesInOrder.Find(s => s.SceneName == sceneName);

            if (scene != null)
            {
                _currentScene = scene;
            }
            else
            {
                Debug.LogError($"Scene with name '{sceneName}' not found.");
            }
        }

        public SceneStaticData GetNextScene()
        {
            if (_currentScene == null)
            {
                Debug.LogError("Current scene is not set.");
                return null;
            }

            int currentIndex = _scenes.ScenesInOrder.IndexOf(_currentScene);

            if (currentIndex >= 0 && currentIndex < _scenes.ScenesInOrder.Count - 1)
            {
                _currentScene = _scenes.ScenesInOrder[currentIndex + 1];
                return _currentScene;
            }

            if (currentIndex == _scenes.ScenesInOrder.Count - 1)
            {
                _currentScene = _scenes.ScenesInOrder[0];
                return _currentScene;
            }

            Debug.LogError("No next scene available.");
            return null;
        }
    }
}
