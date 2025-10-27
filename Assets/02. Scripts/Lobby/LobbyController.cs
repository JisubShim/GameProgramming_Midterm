using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ����ϱ� ���� �߰�
using UnityEngine.SceneManagement;
using TMPro; // �� ������ ���� �߰� (������)

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _bestRecordText;

    private string _prologueSceneName = "PrologueScene";

    void Start()
    {
        if(GameManager.Instance.BestRecord == 0f)
        {
            _bestRecordText.text = "�ְ� ���: -, ��Ͼ���";
        }
        else if (GameManager.Instance.BestRecord <= 140f)
        {
            // A+
            _bestRecordText.text = "�ְ� ���: " + GameManager.Instance.BestRecord.ToString("F1") +"s" + ", A+";
        }
        else if (GameManager.Instance.BestRecord <= 160f)
        {
            // A0
            _bestRecordText.text = "�ְ� ���: " + GameManager.Instance.BestRecord.ToString("F1") + "s" + ", A0";
        }
        else if (GameManager.Instance.BestRecord <= 180f)
        {
            // B+
            _bestRecordText.text = "�ְ� ���: " + GameManager.Instance.BestRecord.ToString("F1") + "s" + ", B+";
        }
        else if (GameManager.Instance.BestRecord <= 200f)
        {
            // B0
            _bestRecordText.text = "�ְ� ���: " + GameManager.Instance.BestRecord.ToString("F1") + "s" + ", B0";
        }
        else
        {
            // �����
            _bestRecordText.text = "�ְ� ���: " + GameManager.Instance.BestRecord.ToString("F1") + "s" + ", '�����'";
        }

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