using UnityEngine;

public class Boss1IdleState : IState
{
    private Boss1StateController _boss1StateController;
    private Boss1 _boss1;
    private float _idleTime = 3f;
    private float _currentTime = 0f;

    private bool _isFirstNextPhase2 = true;
    private bool _isFirstNextPhase3 = true;

    public Boss1IdleState(Boss1StateController boss1StateController, Boss1 boss1)
    {
        _boss1StateController = boss1StateController;
        _boss1 = boss1;
    }

    public void Enter()
    {
        _boss1.Boss1Animator.SetBool("isWalk", true);
        _currentTime = 0f;
    }

    public void Exit()
    {
        _boss1.Boss1Animator.SetBool("isWalk", false);
    }

    public void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _idleTime)
        {
            _boss1StateController.TransitionTo(Boss1State.Attack);
        }

        if(_boss1.CurrentHp <= 7 && _isFirstNextPhase2)
        {
            _isFirstNextPhase2 = false;
            _boss1.Speak("조금 진심을 내볼까?");
            _boss1StateController.GoNextPhase();
        }
        else if(_boss1.CurrentHp <= 4 && _isFirstNextPhase3)
        {
            _isFirstNextPhase3 = false;
            _boss1.Speak("젠장! 내 진심을 보여주마!");
            _boss1StateController.GoNextPhase();
        }
    }
}
