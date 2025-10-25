using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHp = 100f;
    private float _currentHp = 100f;

    private void Awake()
    {
        _currentHp = _maxHp;
    }

    // 플레이어 힐
    public void Heal(int amount)
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
    public void GetDamage(int amount)
    {
        if (_currentHp - amount <= 0)
        {
            // 게임 오버
        }
        else
        {
            _currentHp -= amount;
        }
    }
}
