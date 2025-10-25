using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHp = 100f;
    private float _currentHp = 100f;

    private void Awake()
    {
        _currentHp = _maxHp;
    }

    // �÷��̾� ��
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

    // �÷��̾� ������
    public void GetDamage(int amount)
    {
        if (_currentHp - amount <= 0)
        {
            // ���� ����
        }
        else
        {
            _currentHp -= amount;
        }
    }
}
