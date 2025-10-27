using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("세팅")]
    [SerializeField] private GameController _gameSystem;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private float _maxHp = 100f;
    [SerializeField] private float _hpDecreaseSpeed = 1f;

    [Header("피격 관련")]
    [SerializeField] private float _invincibilityDuration = 1f;
    [SerializeField] private float _blinkSpeed = 0.1f;

    [Header("사격 관련")]
    [SerializeField] private TextMeshProUGUI _bulletCountText;
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private float _gunRotationSpeed = 100f;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _maxBullets = 3;

    private float _currentHp;
    private Animator _playerAnimator;
    private SpriteRenderer _spriteRenderer;
    private bool _isInvincible = false;
    private float _nextFireTime = 0f;

    private bool _canShoot = false;
    private int _bulletsFired = 0;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentHp = _maxHp;
        Initialize();
    }

    public void Initialize()
    {
        _playerAnimator.updateMode = AnimatorUpdateMode.Normal;
        _currentHp = _maxHp;
        _hpBar.value = _currentHp;
        _isInvincible = false;
        _spriteRenderer.color = Color.white;
        _nextFireTime = 0f;

        SetCanShoot(false);
    }

    private void Update()
    {
        if (GameController.IsGameOver) return;

        if (_canShoot)
        {
            _bulletCountText.gameObject.SetActive(true);
            _bulletCountText.text = "총알 x" + (_maxBullets - _bulletsFired);
        }
        else
        {
            _bulletCountText.gameObject.SetActive(false);
        }

            GetDamage(_hpDecreaseSpeed * Time.deltaTime, true);

        HandleGunInput();
    }

    private void HandleGunInput()
    {
        if (!_canShoot) return;

        if (Input.GetKey(KeyCode.Q))
        {
            RotateGun(1);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            RotateGun(-1);
        }

        if (Input.GetKey(KeyCode.W) && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void RotateGun(int direction)
    {
        _gunTransform.Rotate(0, 0, _gunRotationSpeed * direction * Time.deltaTime);
    }

    private void Shoot()
    {
        SoundManager.Instance.PlaySFX("GunShoot");
        GameObject newBullet = Instantiate(_bulletPrefab, _muzzlePoint.position, _muzzlePoint.rotation);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();

        bulletRb.linearVelocity = _muzzlePoint.right * _bulletSpeed;

        _bulletsFired++;
        if (_bulletsFired >= _maxBullets)
        {
            SetCanShoot(false);
        }
    }

    public void SetCanShoot(bool canShoot)
    {
        _canShoot = canShoot;
        _gunTransform.gameObject.SetActive(canShoot);

        if (canShoot)
        {
            _gunTransform.localRotation = Quaternion.Euler(0, 0, 0);
            _bulletsFired = 0;
        }
    }

    public void Heal(float amount)
    {
        _currentHp = Mathf.Min(_currentHp + amount, _maxHp);
        _hpBar.value = _currentHp;
    }

    public void GetDamage(float amount, bool isContinuousDamage = false)
    {
        if (Boss1StateController.IsDead) return;
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
                SoundManager.Instance.PlaySFX("Damage");
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