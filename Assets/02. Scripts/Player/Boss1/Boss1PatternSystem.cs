using UnityEngine;

public class Boss1PatternSystem : MonoBehaviour
{
    [SerializeField] private BigBombSystem _bigBombSystem;
    public bool IsEnd_BigBomb => _bigBombSystem.IsEnd;

    public void PlayDropBombOnPlayer(int count, float duration, float initialWarningTime, float moveSpeed)
    {
        _bigBombSystem.DropBombOnPlayer(count, duration, initialWarningTime, moveSpeed);
    }

    public void PlayDropMultipleBombs(int count, float initialWarningTime, float moveSpeed)
    {
        _bigBombSystem.DropMultipleBombs(count, initialWarningTime, moveSpeed);
    }
}
