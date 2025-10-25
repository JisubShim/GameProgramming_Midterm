using UnityEngine;

public class Boss1PatternSystem : MonoBehaviour
{
    [SerializeField] BigBombSystem _bigBombSystem;

    public void PlayBigBombPattern(int count, float duration)
    {
        _bigBombSystem.Boom(count, duration);
    }
}
