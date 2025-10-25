using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("플레이어 세팅")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speed = 500f;
    private int _maxJumps = 2;
    private bool _isJump = false;

    

    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;
    private int _currentJumps = 0;

    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _isJump = false;
    }

    void Update()
    {
        if (!_isJump && Input.GetKey(KeyCode.D))
        {
            _playerRigidbody.linearVelocity = new Vector2(_speed, _playerRigidbody.linearVelocity.y);
        }
        else if (!_isJump && Input.GetKey(KeyCode.A))
        {
            _playerRigidbody.linearVelocity = new Vector2(-_speed, _playerRigidbody.linearVelocity.y);
        }
        else
        {
            _playerRigidbody.linearVelocity = new Vector2(0f, _playerRigidbody.linearVelocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentJumps < _maxJumps)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        _isJump = true;
        _playerAnimator.SetBool("isGround", false);
        if (_currentJumps == 0)
        {
            _playerAnimator.SetTrigger("isJump");
        }

        if (_currentJumps == 1)
        {
            _playerAnimator.SetTrigger("isDoubleJump");
        }

            _playerRigidbody.linearVelocity = new Vector2(_playerRigidbody.linearVelocity.x, 0);
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);

        _currentJumps++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _playerAnimator.SetBool("isGround", true);
            _isJump = false;
            _currentJumps = 0;
        }
    }
}