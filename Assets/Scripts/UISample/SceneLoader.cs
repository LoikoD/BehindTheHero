using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneChange(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
