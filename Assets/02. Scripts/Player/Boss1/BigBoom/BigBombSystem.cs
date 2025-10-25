using System.Collections;
using UnityEngine;

public class BigBombSystem : MonoBehaviour
{
    [SerializeField] private BigBomb[] _bigBombs;
    private Coroutine _coroutine;

    public void Boom(int count, float duration)
    {
        if (_coroutine != null)
        {
            Debug.LogWarning("���� BigBomb�� ������ �ʾҴµ�, �� ������");
            return;
        }

        _coroutine = StartCoroutine(BoomCoroutine(count, duration));
    }

    private IEnumerator BoomCoroutine(int count, float duration)
    {
        if(count > 10)
        {
            count = 10;
        }

        for(int i = 0; i < count; i++)
        {
            _bigBombs[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(duration);
        }
    }
}