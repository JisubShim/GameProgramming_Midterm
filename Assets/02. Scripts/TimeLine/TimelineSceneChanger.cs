using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineSceneChanger : MonoBehaviour
{
    private string _nextSceneName = "InGameScene";

    public void LoadNextScene()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(_nextSceneName);
        }
    }
}