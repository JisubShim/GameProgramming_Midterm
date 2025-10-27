using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed;
    private float _scrollAmount = 18f;
    private Vector3 _moveDirection = new Vector3(-1f, 0f, 0f);

    private void Update()
    {
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;

        if (transform.position.x <= -_scrollAmount)
        {
            transform.position = _target.position - _moveDirection * _scrollAmount;
        }
    }
}
