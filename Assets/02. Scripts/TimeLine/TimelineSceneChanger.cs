using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineSceneChanger : MonoBehaviour
{
    private string _nextSceneName = "InGameScene";

    private void Start()
    {
        SoundManager.Instance.PlayBGM("PrologueBGM");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadNextScene();
        }
    }
    public void LoadNextScene()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(_nextSceneName);
        }
    }
}