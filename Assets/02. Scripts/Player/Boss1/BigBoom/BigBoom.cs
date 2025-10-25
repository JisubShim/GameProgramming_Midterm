using System.Collections;
using UnityEngine;

public class BigBoom : MonoBehaviour
{
    [SerializeField] private GameObject _warningLine;
    [SerializeField] private float _initialWarningTime = 2f;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private GameObject _player;

    private Animator _bigBoomAnimator;
    private bool _isMoving = false;
    private bool _isBoom = false;

    private void Awake()
    {
        _bigBoomAnimator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        _isBoom = false;
        _isMoving = false;
        gameObject.transform.position = new Vector3(_player.transform.position.x, 6.5f, _player.transform.position.z);
        StartCoroutine(StartWarningCoroutine());
    }

    void Update()
    {
        if (_isBoom)
        {
            return;
        }

        if (_isMoving)
        {
            transform.Translate(Vector2.down * _moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator StartWarningCoroutine()
    {
        _warningLine.SetActive(true);
        yield return new WaitForSeconds(_initialWarningTime);
        _warningLine.SetActive(false);
        _isMoving = true;
    }

    public void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _bigBoomAnimator.SetTrigger("isBoom");
            _isBoom = true;
        }
        else if (collision.CompareTag("Player"))
        {
            _bigBoomAnimator.SetTrigger("isBoom");
            _isBoom = true;
        }
    }
}