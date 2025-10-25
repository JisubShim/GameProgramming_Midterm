using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject[] _obstacleGO;
    [SerializeField] private Transform[] _obstacleTransform;
    [SerializeField] private float _moveSpeed;

    private Vector3 _moveDirection = new Vector3(-1f, 0f, 0f);
    private float _scrollAmount = 18f;
    private List<GameObject> _activeObstacles = new List<GameObject>();

    private bool _isActiveObstacle = true;

    private void Update()
    {
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;

        if (transform.position.x <= -_scrollAmount)
        {
            ClearObstacles();

            transform.position = _target.position - _moveDirection * _scrollAmount;

            if (_isActiveObstacle)
            {
                SpawnSingleRandomObstacle();
            }
        }
    }

    private void ClearObstacles()
    {
        foreach (GameObject obstacle in _activeObstacles)
        {
            Destroy(obstacle);
        }
        _activeObstacles.Clear();
    }

    private void SpawnSingleRandomObstacle()
    {
        if (_obstacleGO.Length == 0 || _obstacleTransform.Length == 0)
        {
            Debug.LogWarning("장애물 프리팹, 배치 위치가 설정되지 않음");
            return;
        }

        int randomPositionIndex = Random.Range(0, _obstacleTransform.Length);
        Transform spawnPoint = _obstacleTransform[randomPositionIndex];

        int randomPrefabIndex = Random.Range(0, _obstacleGO.Length);
        GameObject randomObstaclePrefab = _obstacleGO[randomPrefabIndex];

        GameObject newObstacle = Instantiate(randomObstaclePrefab, spawnPoint.position, spawnPoint.rotation);

        newObstacle.transform.SetParent(this.transform);

        _activeObstacles.Add(newObstacle);
    }

    public void SetActiveObstacle(bool isActive)
    {
        _isActiveObstacle = isActive;
    }
}