using System.Collections;
using UnityEngine;

public class ThrowableBombSystem : MonoBehaviour
{
    [SerializeField] private ThrowableBomb[] _throwableBombs;
    [SerializeField] private Player _player;
    [SerializeField] private Boss1 _boss1;
    private int _bombCount = 0;
    private bool _isEnd = true;
    public bool IsEnd => _isEnd;

    private void Awake()
    {
        _bombCount = _throwableBombs.Length;
        _isEnd = true;
    }

    public void PlayThrowBombToPlayer(int amount, float duration, float arcHeight, float travelTime)
    {
        if (amount > _bombCount)
        {
            amount = _bombCount;
        }

        if (!_isEnd)
        {
            Debug.LogWarning("아직 ThrowBomb가 끝나지 않았는데, 또 시작함: PlayThrowBombToPlayer");
            return;
        }
        _isEnd = false;
        StartCoroutine(ThrowBombToPlayerCoroutine(amount, duration, arcHeight, travelTime));
    }

    private IEnumerator ThrowBombToPlayerCoroutine(int amount, float duration, float arcHeight, float travelTime)
    {
        for (int i = 0; i < amount; i++)
        {
            _throwableBombs[i].gameObject.SetActive(true);
            _throwableBombs[i].Throw(_boss1.gameObject.transform.position, _player.gameObject.transform.position, arcHeight, travelTime);
            yield return new WaitForSeconds(duration);
        }
        _isEnd = true;
    }
}