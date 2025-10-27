using UnityEngine;
using DG.Tweening;

public abstract class Item : MonoBehaviour
{
    [Header("두근거리는 애니메이션 설정")]
    [SerializeField] private float _targetScale = 1.1f;
    [SerializeField] private float _scaleDuration = 0.3f;

    protected abstract void TriggerEffect(Player player);

    private void Start()
    {
        transform.DOScale(_targetScale, _scaleDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX("ItemPickUp");
            TriggerEffect(collision.GetComponent<Player>());
            Destroy(gameObject);
        }
    }
}