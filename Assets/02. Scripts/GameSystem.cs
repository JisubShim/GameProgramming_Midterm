using Unity.VisualScripting;
using UnityEngine;

public class GameSystem : MonoBehaviour 
{
    [SerializeField] private GameObject _gameOverPanel;

    public void GameOver()
    {
        Time.timeScale = 0f;
        _gameOverPanel.SetActive(true);
    }

    public void GameVictory()
    {
        // °ÔÀÓ ½Â¸®
    }
}