using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _damage = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetDamage(_damage);
        }
    }
}