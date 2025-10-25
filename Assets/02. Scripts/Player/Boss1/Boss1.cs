using System.Collections;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private int _maxHp = 10;
    private int _currentHp = 10;

    [Header("피격 효과")]
    [SerializeField] private float _invincibilityDuration = 2f;
    [SerializeField] private float _blinkSpeed = 0.1f;
    private bool _isInvincible = false;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentHp = _maxHp;
    }

    void Update()
    {
        
    }

    public void Heal(int amount)
    {
        _currentHp = Mathf.Min(_currentHp + amount, _maxHp);
    }

    public void GetDamage(int amount, bool isContinuousDamage = false)
    {
        if (!isContinuousDamage && _isInvincible) return;

        _currentHp -= amount;

        if (_currentHp <= 0)
        {
            _currentHp = 0;
            Die();
        }
        else
        {
            if (!isContinuousDamage)
            {
                StartCoroutine(InvincibilityCoroutine());
            }
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _isInvincible = true;

        float timer = 0f;
        Color blinkColor = new Color(1f, 0.2f, 0.2f, 0.5f);

        while (timer < _invincibilityDuration)
        {
            _spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(_blinkSpeed);

            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(_blinkSpeed);

            timer += _blinkSpeed * 2;
        }

        _spriteRenderer.color = Color.white;
        _isInvincible = false;
    }

    private void Die()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GetDamage(1);
            collision.gameObject.SetActive(false);
        }
    }
}
