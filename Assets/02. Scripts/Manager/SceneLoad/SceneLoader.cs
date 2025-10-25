using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private string loadingSceneName = "Loading";

    private SceneFader _sceneFader;

    private Coroutine _loadSceneCoroutine = null;

    private string _currentSceneName = null;

    private void Start()
    {
        var activeScene = SceneManager.GetActiveScene();
        _currentSceneName = activeScene.name;

        _sceneFader = GetComponent<SceneFader>();
    }

    public void LoadScene(string sceneName)
    {
        if (_loadSceneCoroutine == null)
        {
            _loadSceneCoroutine = StartCoroutine(LoadSceneProcess(sceneName));
        }
    }

    private IEnumerator LoadSceneProcess(string sceneName)
    {
        // 현재 씬 로드하면 무시
        if (_currentSceneName == sceneName) yield break;

        Debug.Log($"로딩 씬: {sceneName}");

        try
        {
            // 페이드 아웃
            yield return _sceneFader.Show();

            // 로딩 씬 로드
            yield return SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

            // 현재 씬 언로드
            if (!string.IsNullOrWhiteSpace(_currentSceneName))
                yield return SceneManager.UnloadSceneAsync(_currentSceneName);

            // 다음 씬 로드
            var loadNextScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return loadNextScene;

            // 성공적으로 로드되었는지 확인
            if (loadNextScene.isDone)
            {
                _currentSceneName = sceneName;
            }

            // 로딩 씬 언로드
            yield return SceneManager.UnloadSceneAsync(loadingSceneName);

            // 페이드 인
            yield return _sceneFader.Hide();
        }
        finally
        {
            // 코루틴 종료 후 초기화
            _loadSceneCoroutine = null;
        }
    }
}