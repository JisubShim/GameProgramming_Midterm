using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _bestRecordText;

    private string _prologueSceneName = "PrologueScene";

    void Start()
    {
        SoundManager.Instance.PlayBGM("TitleBGM");
        if(GameManager.Instance.BestRecord == 0f)
        {
            _bestRecordText.text = "최고 기록: -, 기록없음";
        }
        else if (GameManager.Instance.BestRecord <= 120f)
        {
            // A+
            _bestRecordText.text = "최고 기록: " + GameManager.Instance.BestRecord.ToString("F1") +"s" + ", A+";
        }
        else if (GameManager.Instance.BestRecord <= 140f)
        {
            // A0
            _bestRecordText.text = "최고 기록: " + GameManager.Instance.BestRecord.ToString("F1") + "s" + ", A0";
        }
        else if (GameManager.Instance.BestRecord <= 160f)
        {
            // B+
            _bestRecordText.text = "최고 기록: " + GameManager.Instance.BestRecord.ToString("F1") + "s" + ", B+";
        }
        else if (GameManager.Instance.BestRecord <= 180f)
        {
            // B0
            _bestRecordText.text = "최고 기록: " + GameManager.Instance.BestRecord.ToString("F1") + "s" + ", B0";
        }
        else
        {
            // 재수강
            _bestRecordText.text = "최고 기록: " + GameManager.Instance.BestRecord.ToString("F1") + "s" + ", '재수강'";
        }

        _startButton.onClick.AddListener(OnStartButtonClicked);

        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        SoundManager.Instance.PlaySFX("GameStart");
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