using System.Collections;
using UnityEngine;

public class ThrowableBomb : MonoBehaviour
{
    [SerializeField] private float _damage = 8f;
    private Animator _animator;

    private bool _isExploding = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _isExploding = false;
    }

    public void Throw(Vector3 startPoint, Vector3 endPoint, float arcHeight, float travelTime)
    {
        _isExploding = false;
        StartCoroutine(ThrowCoroutine(startPoint, endPoint, arcHeight, travelTime));
    }

    private IEnumerator ThrowCoroutine(Vector3 startPoint, Vector3 endPoint, float arcHeight, float travelTime)
    {
        float timer = 0f;

        endPoint.y = -2.9f;
        Vector3 controlPoint = startPoint + (endPoint - startPoint) / 2 + Vector3.up * arcHeight;

        while (timer < travelTime && !_isExploding)
        {
            float t = timer / travelTime;
            Vector3 pathPart1 = Vector3.Lerp(startPoint, controlPoint, t);
            Vector3 pathPart2 = Vector3.Lerp(controlPoint, endPoint, t);
            transform.position = Vector3.Lerp(pathPart1, pathPart2, t);

            timer += Time.deltaTime;
            yield return null;
        }

        if (!_isExploding)
        {
            transform.position = endPoint;
            Explode();
        }
    }

    private void Explode()
    {
        if (_isExploding) return;

        _isExploding = true;
        _animator.SetTrigger("isBoom");
    }

    // 애니메이션 이벤트 등록함
    public void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetDamage(_damage);
            Explode();
        }
    }
}