using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _controlDescriptionPanel;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject[] _activeFalseGOs;
    [SerializeField] private Player _player;
    private bool _isToggleOn = true;
    private static bool _isGameOver = false;
    public static bool IsGameOver => _isGameOver;
    private void Awake()
    {
        _isGameOver = false;
    }
    public void GameOver()
    {
        _isGameOver = true;
        foreach(var go in _activeFalseGOs)
        {
            go.SetActive(false);
        }
        _player.gameObject.tag = "Untagged";
        _gameOverPanel.SetActive(true);
    }

    public void Update()
    {
        _timerText.text = GameManager.Instance.Timer.ToString("F1");

        if (!_isGameOver && Input.GetKeyDown(KeyCode.Tab))
        {
            _isToggleOn = !_isToggleOn;
            _controlDescriptionPanel.SetActive(_isToggleOn);
        }
        
        if (_isGameOver && Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneLoader.Instance.LoadScene("InGameScene");
        }
        else if (_isGameOver && Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneLoader.Instance.LoadScene("TitleScene");
        }
    }
}