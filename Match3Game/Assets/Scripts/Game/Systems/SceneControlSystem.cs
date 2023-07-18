using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlSystem : ISceneControlSystem
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
