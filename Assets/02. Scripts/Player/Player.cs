using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameSystem _gameSystem;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private float _maxHp = 100f;
    [SerializeField] private float _hpDecreaseSpeed = 1f;
    private float _currentHp = 100f;
    
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerAnimator.updateMode = AnimatorUpdateMode.Normal;
        _currentHp = _maxHp;
    }

    private void Update()
    {
        // 테스트용. 지워야 함
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetDamage(100000f);
        }

        GetDamage(_hpDecreaseSpeed * Time.deltaTime);
    }

    // 플레이어 힐
    public void Heal(float amount)
    {
        if (amount + _currentHp > _maxHp)
        {
            _currentHp = _maxHp;
        }
        else
        {
            _currentHp += amount;
        }
    }

    // 플레이어 데미지
    public void GetDamage(float amount)
    {
        if (_currentHp - amount <= 0f)
        {
            _currentHp = 0f;
            _hpBar.value = _currentHp;
            _playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            _playerAnimator.SetTrigger("isDead");
            _gameSystem.GameOver();
        }
        else
        {
            _currentHp -= amount;
            _hpBar.value = _currentHp;
        }
    }
}
