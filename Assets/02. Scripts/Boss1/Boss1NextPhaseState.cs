using UnityEngine;

public class Boss1NextPhaseState : IBossState
{
    private Boss1StateController _boss1StateController;
    private Boss1 _boss1;
    private Boss1PatternSystem _patternSystem;
    private float _waitTime = 3f;
    private float _currentTime = 0f;

    public Boss1NextPhaseState(Boss1StateController boss1StateSystem, Boss1 boss1, Boss1PatternSystem patternSystem)
    {
        _boss1StateController = boss1StateSystem;
        _boss1 = boss1;
        _patternSystem = patternSystem;
    }

    public void Enter()
    {
        SoundManager.Instance.PlaySFX("NextPhase");
        _currentTime = 0f;
        _boss1StateController.GoNextPhase();
        _boss1.ActivateInvincible(true);
        _patternSystem.StopAll();
    }

    public void Exit()
    {
        _boss1.ActivateInvincible(false);
    }

    public void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _waitTime)
        {
            _boss1StateController.TransitionTo(Boss1State.Idle);
        }
    }
}
