using Unity.VisualScripting;
using UnityEngine;

public class GameOverController : MonoBehaviour 
{
    [SerializeField] private GameObject _gameOverPanel;
    private bool _isGameOver = false;

    public void GameOver()
    {
        _isGameOver = true;
        _gameOverPanel.SetActive(true);
    }

    public void Update()
    {
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