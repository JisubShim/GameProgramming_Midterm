using UnityEngine;

public class Boss1Phase3AttackState : IBossState
{
    private Boss1StateController _boss1StateController;
    private Boss1 _boss1;
    private Boss1PatternSystem _patternSystem;
    private int _playCount = 0;

    public Boss1Phase3AttackState(Boss1StateController boss1StateSystem, Boss1 boss1, Boss1PatternSystem patternSystem)
    {
        _boss1StateController = boss1StateSystem;
        _boss1 = boss1;
        _patternSystem = patternSystem;
    }

    public void Enter()
    {
        _boss1.Boss1Animator.SetBool("isAttack", true);

        if (_playCount % 2 == 0)
        {
            _boss1.Speak("머리 위 폭탄 조심하라고~");
            _patternSystem.PlayDropBombOnPlayer(count: 4, duration: 2f, initialWarningTime: 1f, moveSpeed: 20f);
        }
        else if (_playCount % 2 == 1)
        {
            _boss1.Speak("이것도 피해보시지~");
            _patternSystem.PlayDropMultipleBombs(count: 4, initialWarningTime: 1.5f, moveSpeed: 20f);
        }
        _playCount++;
    }

    public void Exit()
    {
        _boss1.Boss1Animator.SetBool("isAttack", false);
    }

    public void Update()
    {
        if (_patternSystem.IsEnd_BigBomb)
        {
            _boss1StateController.TransitionTo(Boss1State.Idle);
        }
    }
}