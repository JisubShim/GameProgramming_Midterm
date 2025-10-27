using System.Collections;
using TMPro;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private int _maxHp = 12;
    private int _currentHp;
    public int CurrentHp => _currentHp;

    [Header("피격 효과")]
    [SerializeField] private GameObject _barrierGO;
    [SerializeField] private float _invincibilityDuration = 2f;
    [SerializeField] private float _blinkSpeed = 0.1f;
    private bool _isInvincible = false;
    private SpriteRenderer _spriteRenderer;
    private Animator _boss1Animator;
    public Animator Boss1Animator => _boss1Animator;

    [Header("대사 관련")]
    [SerializeField] private GameObject _dialogueTextGO;
    [SerializeField] private TextMeshProUGUI _boss1DialogueTMP;
    [SerializeField] private TextMeshProUGUI _boss1HpTMP;
    [SerializeField] private float _dialogueTextRunTime = 3f;

    private Coroutine _invincibleCoroutine;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boss1Animator = GetComponent<Animator>();
        _currentHp = _maxHp;
    }

    void Update()
    {
        _boss1HpTMP.text = "HP: " + _currentHp;
    }

    public void Heal(int amount)
    {
        _currentHp = Mathf.Min(_currentHp + amount, _maxHp);
    }

    public void GetDamage(int amount, bool isContinuousDamage = false)
    {
        if (!isContinuousDamage && _isInvincible) return;

        _currentHp -= amount;

        if (!isContinuousDamage)
        {
            _invincibleCoroutine = StartCoroutine(InvincibilityCoroutine(_invincibilityDuration));
        }
    }

    public void ActivateInvincible(bool isInvincible)
    {
        StopCoroutine(_invincibleCoroutine);
        _barrierGO.SetActive(isInvincible);
        _isInvincible = isInvincible;
        _spriteRenderer.color = Color.white;
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration)
    {
        _isInvincible = true;

        float timer = 0f;
        Color blinkColor = new Color(1f, 0.2f, 0.2f, 0.5f);

        while (timer < invincibilityDuration)
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

    // 대사 말하기
    public void Speak(string dialogue)
    {
        StartCoroutine(SpeakCoroutine(dialogue));
    }

    private IEnumerator SpeakCoroutine(string dialogue)
    {
        _boss1DialogueTMP.text = dialogue;
        _dialogueTextGO.SetActive(true);
        yield return new WaitForSeconds(_dialogueTextRunTime);
        _dialogueTextGO.SetActive(false);
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
