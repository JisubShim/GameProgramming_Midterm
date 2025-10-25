using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ����ϱ� ���� �߰�
using UnityEngine.SceneManagement; // �� ������ ���� �߰� (������)

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private string _prologueSceneName = "PrologueScene";

    void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);

        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        SceneLoader.Instance.LoadScene(_prologueSceneName);
    }

    private void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnDestroy()
    {
        if (_startButton != null)
        {
            _startButton.onClick.RemoveListener(OnStartButtonClicked);
        }
        if (_exitButton != null)
        {
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }
    }
}