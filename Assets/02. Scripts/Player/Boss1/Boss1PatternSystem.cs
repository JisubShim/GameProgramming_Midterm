using UnityEngine;

public class Boss1PatternSystem : MonoBehaviour
{
    [SerializeField] private BigBombSystem _bigBombSystem;
    [SerializeField] private ThrowableBombSystem _throwableBombSystem;
    public bool IsEnd_BigBomb => _bigBombSystem.IsEnd;
    public bool IsEnd_ThrowableBomb => _throwableBombSystem.IsEnd;
    public bool IsEnd => _bigBombSystem.IsEnd && _throwableBombSystem.IsEnd;

    // BigBombSystem
    public void PlayDropBombOnPlayer(int count, float duration, float initialWarningTime, float moveSpeed)
    {
        _bigBombSystem.DropBombOnPlayer(count, duration, initialWarningTime, moveSpeed);
    }

    public void PlayDropMultipleBombs(int count, float initialWarningTime, float moveSpeed)
    {
        _bigBombSystem.DropMultipleBombs(count, initialWarningTime, moveSpeed);
    }

    // ThrowableBombSystem
    public void PlayThrowBombToPlayer(int amount, float duration, float arcHeight, float travelTime)
    {
        _throwableBombSystem.PlayThrowBombToPlayer(amount, duration, arcHeight, travelTime);
    }
}
