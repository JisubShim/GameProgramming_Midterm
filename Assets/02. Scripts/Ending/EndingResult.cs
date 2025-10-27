using TMPro;
using UnityEngine;

public class EndingResult : MonoBehaviour
{
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _resultText;
    private bool _isEnd = false;

    void Awake()
    {
        _isEnd = false;
        _timerText.text = "�ҿ� �ð�\n" + GameManager.Instance.Record.ToString("F1");
        if(GameManager.Instance.Record <= 120f)
        {
            // A+
            _resultText.text = "��� ���\nA+";
        }
        else if(GameManager.Instance.Record <= 140f)
        {
            // A0
            _resultText.text = "��� ���\nA0";
        }
        else if (GameManager.Instance.Record <= 160f)
        {
            // B+
            _resultText.text = "��� ���\nB+";
        }
        else if (GameManager.Instance.Record <= 180f)
        {
            // B0
            _resultText.text = "��� ���\nB0";
        }
        else
        {
            // �����
            _resultText.text = "��� ���\n'�����'";
        }
    }

    void Update()
    {
        if(_isEnd && Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneLoader.Instance.LoadScene("TitleScene");
        }
    }

    public void ShowResult()
    {
        _resultPanel.SetActive(true);
        _isEnd = true;
    }
}
