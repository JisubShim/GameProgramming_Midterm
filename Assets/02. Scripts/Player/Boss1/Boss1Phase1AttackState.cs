using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Boss1Phase1AttackState : IBossState
{
    private Boss1StateController _boss1StateController;
    private Boss1 _boss1;
    private Boss1PatternSystem _patternSystem;
    private int _playCount = 0;

    public Boss1Phase1AttackState(Boss1StateController boss1StateController, Boss1 boss1, Boss1PatternSystem patternSystem)
    {
        _boss1StateController = boss1StateController;
        _boss1 = boss1;
        _patternSystem = patternSystem;
    }

    public void Enter()
    {
        _boss1.Boss1Animator.SetBool("isAttack", true);

        if (_playCount % 2 == 0)
        {
            _boss1.Speak("머리 위 폭탄 조심하라고~");
            _patternSystem.PlayDropBombOnPlayer(count: 3, duration: 3f, initialWarningTime: 2f, moveSpeed: 10f);
            //_patternSystem.PlayThrowBombToPlayer(3, 4f, 8f, 2f);
        }
        else if(_playCount % 2 == 1)
        {
            _boss1.Speak("이것도 피해보시지~");
            _patternSystem.PlayDropMultipleBombs(count: 2, initialWarningTime: 2f, moveSpeed: 10f);
        }
        _playCount++;
    }

    public void Exit()
    {
        _boss1.Boss1Animator.SetBool("isAttack", false);
    }

    public void Update()
    {
        if (_patternSystem.IsEnd)
        {
            _boss1StateController.TransitionTo(Boss1State.Idle);
        }
    }
}