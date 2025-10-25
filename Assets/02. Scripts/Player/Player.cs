using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private GameSystem _gameSystem;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private float _maxHp = 100f;
    [SerializeField] private float _hpDecreaseSpeed = 1f;

    [Header("피격 효과")]
    [SerializeField] private float _invincibilityDuration = 2f;
    [SerializeField] private float _blinkSpeed = 0.1f;

    private float _currentHp;
    private Animator _playerAnimator;
    private SpriteRenderer _spriteRenderer;
    private bool _isInvincible = false;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentHp = _maxHp;
    }

    public void Initialize()
    {
        _playerAnimator.updateMode = AnimatorUpdateMode.Normal;
        _currentHp = _maxHp;
        _hpBar.value = _currentHp;
        _isInvincible = false;
        _spriteRenderer.color = Color.white;
    }

    private void Update()
    {
        GetDamage(_hpDecreaseSpeed * Time.deltaTime, true);
    }

    public void Heal(float amount)
    {
        _currentHp = Mathf.Min(_currentHp + amount, _maxHp);
        _hpBar.value = _currentHp;
    }

    public void GetDamage(float amount, bool isContinuousDamage = false)
    {
        if (!isContinuousDamage && _isInvincible) return;

        _currentHp -= amount;

        if (_currentHp <= 0f)
        {
            _currentHp = 0f;
            Die();
        }
        else
        {
            _hpBar.value = _currentHp;
            if (!isContinuousDamage)
            {
                StartCoroutine(InvincibilityCoroutine());
            }
        }
    }

    private void Die()
    {
        _hpBar.value = _currentHp;
        _playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        _playerAnimator.SetTrigger("isDead");
        _gameSystem.GameOver();
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
}