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
        // ���� �� �ε��ϸ� ����
        if (_currentSceneName == sceneName) yield break;

        Debug.Log($"�ε� ��: {sceneName}");

        try
        {
            // ���̵� �ƿ�
            yield return _sceneFader.Show();

            // �ε� �� �ε�
            yield return SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

            // ���� �� ��ε�
            if (!string.IsNullOrWhiteSpace(_currentSceneName))
                yield return SceneManager.UnloadSceneAsync(_currentSceneName);

            // ���� �� �ε�
            var loadNextScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return loadNextScene;

            // ���������� �ε�Ǿ����� Ȯ��
            if (loadNextScene.isDone)
            {
                _currentSceneName = sceneName;
            }

            // �ε� �� ��ε�
            yield return SceneManager.UnloadSceneAsync(loadingSceneName);

            // ���̵� ��
            yield return _sceneFader.Hide();
        }
        finally
        {
            // �ڷ�ƾ ���� �� �ʱ�ȭ
            _loadSceneCoroutine = null;
        }
    }
}