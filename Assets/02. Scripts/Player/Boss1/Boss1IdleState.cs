using UnityEngine;

public class Boss1IdleState : IState
{
    private Boss1StateController _boss1StateController;
    private Boss1 _boss1;
    private float _idleTime = 3f;
    private float _currentTime = 0f;

    public Boss1IdleState(Boss1StateController boss1StateController, Boss1 boss1)
    {
        _boss1StateController = boss1StateController;
        _boss1 = boss1;
    }

    public void Enter()
    {
        _boss1StateController.SetActiveObstacle(true);
        _currentTime = 0f;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _idleTime)
        {
            _boss1StateController.TransitionTo(Boss1State.Attack);
        }
    }
}
