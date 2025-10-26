using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BigBombType
{
    DropBombOnPlayer,
    DropMultipleBombs
}
public class BigBombSystem : MonoBehaviour
{
    [SerializeField] private BigBomb[] _bigBombs;
    private Coroutine _coroutine;
    private int _bombCount = 0;
    private bool _isEnd = true;
    public bool IsEnd => _isEnd;
    private void Awake()
    {
        _bombCount = _bigBombs.Length;
        _isEnd = true;
    }

    // 플레이어 x축 위치 추적 후 수직 낙하
    public void DropBombOnPlayer(int count, float duration, float initialWarningTime = 2f, float moveSpeed = 10f)
    {
        if (!_isEnd)
        {
            Debug.LogWarning("아직 BigBomb이 끝나지 않았는데, 또 시작함: DropBombOnPlayer");
            return;
        }
        _isEnd = false;
        _coroutine = StartCoroutine(DropBombOnPlayerCoroutine(count, duration, initialWarningTime, moveSpeed));
    }

    private IEnumerator DropBombOnPlayerCoroutine(int count, float duration, float initialWarningTime = 2f, float moveSpeed = 10f)
    {
        Debug.Log("DropBombOnPlayerCoroutine");
        if(count > _bombCount)
        {
            count = _bombCount;
        }

        for(int i = 0; i < count; i++)
        {
            _bigBombs[i].LotateOnPlayer();
            _bigBombs[i].gameObject.SetActive(true);
            _bigBombs[i].StartBoom(initialWarningTime, moveSpeed);
            yield return new WaitForSeconds(duration);
        }
        _isEnd = true;
    }

    public void DropMultipleBombs(int count, float initialWarningTime = 2f, float moveSpeed = 10f)
    {
        if (!_isEnd)
        {
            Debug.LogWarning("아직 BigBomb이 끝나지 않았는데, 또 시작함: DropMultipleBombs");
            return;
        }
        _isEnd = false;
        _coroutine = StartCoroutine(DropMultipleBombsCoroutine(count, initialWarningTime, moveSpeed));
    }

    private IEnumerator DropMultipleBombsCoroutine(int count, float initialWarningTime, float moveSpeed)
    {
        Debug.Log("DropMultipleBombsCoroutine");
        if (count > _bombCount)
        {
            count = _bombCount;
        }

        List<int> indices = new List<int>();
        for (int i = 0; i < _bombCount; i++)
        {
            indices.Add(i);
        }

        for (int i = 0; i < indices.Count; i++)
        {
            int randomIndex = Random.Range(i, indices.Count);
            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        for (int i = 0; i < count; i++)
        {
            int bombIndex = indices[i];
            _bigBombs[bombIndex].gameObject.SetActive(true);
            _bigBombs[bombIndex].StartBoom(initialWarningTime, moveSpeed);
        }

        yield return new WaitForSeconds(3f);
        _isEnd = true;
    }
}