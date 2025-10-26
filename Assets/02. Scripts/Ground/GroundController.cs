using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSpawnData
{
    public GameObject itemPrefab;
    [Range(0, 100)]
    public int spawnWeight = 100;
}

public class GroundController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [Header("물건 장애물 관련")]
    [SerializeField] private GameObject[] _obstacleGO;
    [SerializeField] private Transform[] _obstacleTransform;

    [Header("아이템 관련")]
    [SerializeField] private ItemSpawnData[] _itemSpawnDatas;
    [SerializeField] private Transform[] _itemTransform;

    [Header("설정")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _itemSpawnInterval = 10f;

    private Vector3 _moveDirection = new Vector3(-1f, 0f, 0f);
    private float _scrollAmount = 18f;
    private List<GameObject> _activeObstacles = new List<GameObject>();
    private List<GameObject> _activeItems = new List<GameObject>();

    private bool _isActiveObstacle = true;

    private static float _itemSpawnTimer;
    private static bool _canSpawnItem = false;

    private GameObject _lastSpawnedItemPrefab = null;
    private int _consecutiveItemSpawnCount = 0;

    private void Start()
    {
        _canSpawnItem = false;
        _itemSpawnTimer = _itemSpawnInterval;
    }

    private void Update()
    {
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;

        if (_itemSpawnTimer > 0)
        {
            _itemSpawnTimer -= Time.deltaTime / 2f;
            Debug.Log(_itemSpawnTimer);
        }
        else
        {
            _canSpawnItem = true;
        }

        if (transform.position.x <= -_scrollAmount)
        {
            ClearObstacles();
            ClearItems();

            transform.position = _target.position - _moveDirection * _scrollAmount;

            if (_isActiveObstacle)
            {
                SpawnSingleRandomObstacle();
            }

            if (_canSpawnItem)
            {
                SpawnSingleRandomItem();
                _canSpawnItem = false;
                _itemSpawnTimer = _itemSpawnInterval;
            }
        }
    }

    private void ClearObstacles()
    {
        foreach (GameObject obstacle in _activeObstacles)
        {
            if (obstacle != null)
            {
                Destroy(obstacle);
            }
        }
        _activeObstacles.Clear();
    }

    private void ClearItems()
    {
        foreach (GameObject item in _activeItems)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        _activeItems.Clear();
    }

    private void SpawnSingleRandomObstacle()
    {
        int randomPositionIndex = Random.Range(0, _obstacleTransform.Length);
        Transform spawnPoint = _obstacleTransform[randomPositionIndex];

        int randomPrefabIndex = Random.Range(0, _obstacleGO.Length);
        GameObject randomObstaclePrefab = _obstacleGO[randomPrefabIndex];

        GameObject newObstacle = Instantiate(randomObstaclePrefab, spawnPoint.position, spawnPoint.rotation);

        newObstacle.transform.SetParent(this.transform);

        _activeObstacles.Add(newObstacle);
    }

    private void SpawnSingleRandomItem()
    {
        List<ItemSpawnData> availableItems = new List<ItemSpawnData>();
        foreach (ItemSpawnData data in _itemSpawnDatas)
        {
            if (_lastSpawnedItemPrefab != null && data.itemPrefab == _lastSpawnedItemPrefab && _consecutiveItemSpawnCount >= 2)
            {
                continue;
            }
            availableItems.Add(data);
        }

        int totalWeight = 0;
        foreach (ItemSpawnData data in availableItems)
        {
            totalWeight += data.spawnWeight;
        }

        if (totalWeight <= 0)
        {
            Debug.LogWarning("모든 아이템의 가중치 합이 0 이하 또는 사용 가능한 아이템이 없음. 아이템 생성 불가");
            _lastSpawnedItemPrefab = null;
            _consecutiveItemSpawnCount = 0;
            return;
        }

        int randomWeight = Random.Range(0, totalWeight);
        GameObject selectedItemPrefab = null;

        foreach (ItemSpawnData data in availableItems)
        {
            if (randomWeight < data.spawnWeight)
            {
                selectedItemPrefab = data.itemPrefab;
                break;
            }
            randomWeight -= data.spawnWeight;
        }

        if (selectedItemPrefab == null)
        {
            Debug.LogWarning("확률 계산 실패");
            _lastSpawnedItemPrefab = null;
            _consecutiveItemSpawnCount = 0;
            return;
        }

        if (selectedItemPrefab == _lastSpawnedItemPrefab)
        {
            _consecutiveItemSpawnCount++;
        }
        else
        {
            _consecutiveItemSpawnCount = 1;
        }
        _lastSpawnedItemPrefab = selectedItemPrefab;

        int randomPositionIndex = Random.Range(0, _itemTransform.Length);
        Transform spawnPoint = _itemTransform[randomPositionIndex];

        GameObject newItem = Instantiate(selectedItemPrefab, spawnPoint.position, spawnPoint.rotation);

        newItem.transform.SetParent(this.transform);

        _activeItems.Add(newItem);
    }

    public void SetActiveObstacle(bool isActive)
    {
        _isActiveObstacle = isActive;
    }
}