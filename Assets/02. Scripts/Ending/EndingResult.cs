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
        _timerText.text = "소요 시간\n" + GameManager.Instance.Record.ToString("F1");
        if(GameManager.Instance.Record <= 120f)
        {
            // A+
            _resultText.text = "결과 등급\nA+";
        }
        else if(GameManager.Instance.Record <= 140f)
        {
            // A0
            _resultText.text = "결과 등급\nA0";
        }
        else if (GameManager.Instance.Record <= 160f)
        {
            // B+
            _resultText.text = "결과 등급\nB+";
        }
        else if (GameManager.Instance.Record <= 180f)
        {
            // B0
            _resultText.text = "결과 등급\nB0";
        }
        else
        {
            // 재수강
            _resultText.text = "결과 등급\n'재수강'";
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
