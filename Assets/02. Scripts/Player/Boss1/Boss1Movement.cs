using UnityEngine;

public class Boss1Movement : MonoBehaviour
{
    private Boss1StateController _stateController;

    [Header("Phase3 움직임 세팅")]
    [SerializeField] private float _verticalSpeed = 3f;
    [SerializeField] private float _movementDistance = 3f;

    private float _peakY;

    private void Awake()
    {
        _stateController = GetComponent<Boss1StateController>();
        _peakY = transform.position.y;
    }

    private void Update()
    {
        if (_stateController.Phase == 3)
        {
            float cosValue = Mathf.Cos(Time.time * _verticalSpeed);

            float movementFactor = (cosValue - 1f) * 0.5f;

            float newY = _peakY + movementFactor * _movementDistance;

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}